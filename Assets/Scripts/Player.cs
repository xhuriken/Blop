using Cinemachine;
using DG.Tweening;
using Febucci.UI;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    private List<Rigidbody2D> points = new List<Rigidbody2D>(); // liste des points du blob
    private Dictionary<SpringJoint2D, float> springDistance = new Dictionary<SpringJoint2D, float>();

    private Rigidbody2D m_rb;
    private PlayerInput playerInput;
    public ParticleSystem deathParticles;
    public GameObject transition;
    public float maxSpeed = 3f;
    public float acceleration = 5.0f;
    public float deceleration = 15.0f;
    public float envDeceleration = 15.0f;
    public float growthFactor = 1.2f;
    public float shrinkFactor = 0.8f;
    public Ease growEase = Ease.InElastic;
    public Ease shrinkEase = Ease.OutExpo;
    public float growTime = 0.1f;
    public float shrinkTime = 0.1f;
    private float newEyesVelocityX = 0f;
    private float targetVelocity = 0f;
    private float currentVelocity = 0f;
    private bool isMoving = false;
    private bool isGrowing = false;
    public bool isDead = false;
    private Animator anim;
    public AnimatorController animRed;
    public SpriteShapeRenderer spriteRender;
    private bool isInAir = false;
    //private CinemachineVirtualCamera Room1Vcam;

    private int groundedPointsCount = 0;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        AdjustSpringJointsShrink(shrinkFactor);
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        CheckPlayerID();
        DontDestroyOnLoad(this.gameObject);

        foreach (Transform child in transform)
        {
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                points.Add(rb);
                
            }

            foreach (SpringJoint2D joint in child.GetComponents<SpringJoint2D>())
            {
                springDistance[joint] = joint.distance;
            }
        }

        foreach (var segment in springDistance)
        {
            SpringJoint2D joint = segment.Key;
            float originalDistance = segment.Value;
            joint.distance = originalDistance * shrinkFactor;
        }
    }
    private void CheckPlayerID()
    {
        GameObject[] blueBlops = GameObject.FindGameObjectsWithTag("BlueBlop");

        if (blueBlops.Length > 1) 
        {
            gameObject.tag = "RedBlop";
            this.gameObject.layer = 9;
            Debug.Log($"Player {playerInput.playerIndex} changed to RedBlop");

            anim.runtimeAnimatorController = animRed;
            spriteRender.color = new Color32(217, 66, 92, 255);

            foreach (Transform child in transform)
            {
                Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.gameObject.layer = 9;
                    rb.gameObject.GetComponent<SpriteRenderer>().color = new Color32(217, 66, 92, 255);
                }

                foreach (SpringJoint2D joint in child.GetComponents<SpringJoint2D>())
                {
                    springDistance[joint] = joint.distance;
                }
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float moveInput = context.ReadValue<Vector2>().x;
            targetVelocity = moveInput * maxSpeed;
            isMoving = true;
        }
        else if (context.canceled)
        {
            isMoving = false;
        }
    }

    private void Update()
    {
        if (isDead)
        {
            m_rb.velocity = Vector2.zero;
            return;
        }

        newEyesVelocityX = currentVelocity + m_rb.velocity.x;

        if (isMoving)
        {
            // Accélération
            currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            // Décélération
            currentVelocity = Mathf.Lerp(currentVelocity, 0, deceleration * Time.deltaTime);
            newEyesVelocityX = Mathf.Lerp(newEyesVelocityX, 0, envDeceleration * Time.deltaTime);
        }

        newEyesVelocityX = Mathf.Clamp(newEyesVelocityX, -6, 6);


        if (isGrowing && groundedPointsCount >= 8)
        {
            float stopFactor = 100f * Time.deltaTime;
            m_rb.velocity = Vector2.MoveTowards(m_rb.velocity, Vector2.zero, stopFactor);

            foreach (Rigidbody2D point in points)
            {
                point.velocity = Vector2.MoveTowards(m_rb.velocity, Vector2.zero, stopFactor);
            }
        }
        else
        {
            m_rb.velocity = new Vector2(newEyesVelocityX, m_rb.velocity.y);
        }

        if(groundedPointsCount == 0)
        {
            isInAir = true;
        }
        if(groundedPointsCount > 0 && isInAir == true && (m_rb.velocity.y > 4f || m_rb.velocity.y < -4f || m_rb.velocity.x > 4f || m_rb.velocity.x < -4f))
        {
            anim.SetTrigger("Tuch");
            isInAir = false;
        }

        anim.SetBool("isDead", isDead);
    }   

    public void GrowUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isGrowing = true;
            anim.SetBool("isGrowing", isGrowing);
            AdjustSpringJointsGrow(growthFactor);
            Gamepad.current?.SetMotorSpeeds(0.3f, 0.3f);
        }
        else if (context.canceled)
        {
            isGrowing = false;
            anim.SetBool("isGrowing", isGrowing);
            AdjustSpringJointsShrink(shrinkFactor);
            Gamepad.current?.SetMotorSpeeds(0.0f, 0.0f);
        }
    }

    private void AdjustSpringJointsGrow(float factor)
    {
        foreach (var segment in springDistance)
        {
            SpringJoint2D joint = segment.Key;
            float originalDistance = segment.Value;
            float targetDistance = originalDistance * factor;
            joint.DOKill();
            DOTween.To(() => joint.distance, x => joint.distance = x, targetDistance, growTime)
               .SetEase(growEase).SetTarget(joint);
            //joint.distance = originalDistance * factor;
        }
    }
    private void AdjustSpringJointsShrink(float factor)
    {
        foreach (var segment in springDistance)
        {
            SpringJoint2D joint = segment.Key;
            float originalDistance = segment.Value;
            float targetDistance = originalDistance * factor;
            joint.DOKill();
            DOTween.To(() => joint.distance, x => joint.distance = x, targetDistance, shrinkTime)
               .SetEase(shrinkEase).SetTarget(joint);
            //joint.distance = originalDistance * factor;
        }
    }   

    public void NotifyPointGrounded()
    {
        groundedPointsCount++;
    }

    public void NotifyPointUngrounded()
    {
        groundedPointsCount--;
        if (groundedPointsCount < 0)
            groundedPointsCount = 0;
    }

    public void Die()
    {
        if(isDead)
        {
            return;
        }
        GameManager.Instance.isPlayerDead = true;
        Debug.Log("___Player died In PlayerScript");

        deathParticles.Play();
        StartCoroutine(Dispawn());
        isDead = true;
        
    }

    public IEnumerator Dispawn()
    {

        //foreach (Transform child in transform)
        //{
        //    Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
        //    if (rb != null)
        //    {
        //        rb.isKinematic = true;
        //    }
        //}
        anim.SetBool("isDead", true);
        spriteRender.color = new Color32(255, 255, 255, 0);
        yield return null;
        
        //float duration = 0.2f; 
        //float elapsedTime = 0f;
        //Vector3 initialScale = transform.localScale;
        //Vector3 targetScale = new Vector3(0.25f, 0.25f, 0.25f); 

        //while (elapsedTime < duration)
        //{
        //    transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
        //    elapsedTime += Time.deltaTime;
        //    yield return null;
        //}

        //transform.localScale = targetScale;
        StartCoroutine(Respawn());
        Debug.Log("___LaunchRespawn");

    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.8f);
        Instantiate(transition, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        RespawnPlayer();
        //transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        if (gameObject.tag == "BlueBlop")
        {
            spriteRender.color = new Color32(65, 207, 207, 255);
        }
        if (gameObject.tag == "RedBlop")
        {
            spriteRender.color = new Color32(217, 66, 92, 255);
        }
        //spriteRender.color = new Color32(255, 255, 255, 0);
        //string currentSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(GameManager.Instance.reload());
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        Debug.Log("___RealoadMade");
    }

    public void RespawnPlayer()
    {
        //GameObject[] newLevels = GameObject.FindGameObjectsWithTag("NewLevel");
        //GameObject activeLevel = null;
        //foreach (GameObject level in newLevels)
        //{
        //    NewLevel ld = level.GetComponent<NewLevel>();
        //    if (ld != null && ld.isLevel)
        //    {
        //        activeLevel = level;
        //        Debug.Log("___" + activeLevel.name);
        //        break;
        //    }
        //}
        //if (activeLevel == null) return;
        //Debug.Log("__activeLevelTrouvé");
        //Vec spawnPoint1 = 
        //Transform spawnPoint2 = activeLeve.
        
        GameObject bluePlayer = GameObject.FindGameObjectWithTag("BlueBlop");
        GameObject redPlayer = GameObject.FindGameObjectWithTag("RedBlop");
        //NewLevel newLevel = activeLevel.GetComponent<NewLevel>();
        if (bluePlayer == null)
        {
            Debug.Log("___BluePlayerNotFound");
            return;
        }
        if (redPlayer == null)
        {
            Debug.Log("___BluePlayerNotFound");
            return;
        }
        bluePlayer.transform.position = GameManager.Instance.currentRoom.spawnPoint1;
        redPlayer.transform.position = GameManager.Instance.currentRoom.spawnPoint2;
        //foreach (Transform child in transform)
        //{
        //    Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
        //    if (rb != null)
        //    {
        //        rb.isKinematic = false;
        //    }
        //}
        //GameManager.Instance.CurrentLevel.vcam.Priority = 10;
        Debug.Log("___Finish");
        anim.SetBool("isDead", false);
        isDead = false;
        GameManager.Instance.isPlayerDead = false;

    }

}

