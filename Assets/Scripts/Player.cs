using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private List<Rigidbody2D> points = new List<Rigidbody2D>(); // liste des points du blob
    private Dictionary<SpringJoint2D, float> springDistance = new Dictionary<SpringJoint2D, float>();

    private Rigidbody2D m_rb;

    public ParticleSystem deathParticles;
    public float maxSpeed = 3f;
    public float acceleration = 5.0f;
    public float deceleration = 15.0f;
    public float envDeceleration = 15.0f;
    public float growthFactor = 1.2f;
    public float shrinkFactor = 0.8f;

    private float newEyesVelocityX = 0f;
    private float targetVelocity = 0f;
    private float currentVelocity = 0f;
    private bool isMoving = false;
    private bool isGrowing = false;
    private bool isDead = false;

    private int groundedPointsCount = 0;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        m_rb = GetComponent<Rigidbody2D>();

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

        newEyesVelocityX = Mathf.Clamp(newEyesVelocityX, -4, 4);


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
    }   

    public void GrowUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isGrowing = true;
            AdjustSpringJoints(growthFactor);
        }
        else if (context.canceled)
        {
            isGrowing = false;
            AdjustSpringJoints(shrinkFactor);
        }
    }

    private void AdjustSpringJoints(float factor)
    {
        foreach (var segment in springDistance)
        {
            SpringJoint2D joint = segment.Key;
            float originalDistance = segment.Value;
            joint.distance = originalDistance * factor;
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

        Instantiate(deathParticles, transform.position, Quaternion.identity);
        isDead = true;
        
    }

    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
