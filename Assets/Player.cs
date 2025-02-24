using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private List<Rigidbody2D> points = new List<Rigidbody2D>(); // liste des points du blop (on g�re que les RB)
    private Dictionary<SpringJoint2D, float> springDistance = new Dictionary<SpringJoint2D, float>();

    private Rigidbody2D eyes;

    public float maxSpeed = 3f;
    public float acceleration = 5.0f; 
    public float deceleration = 15.0f; 
    public float growthFactor = 1.2f;
    public float shrinkFactor = 0.8f;

    private float newEyesVelocityX = 0f;
    private float targetDeceleration = 0f;
    private float targetVelocity = 0f;
    private float currentVelocity = 0f;
    private bool isMoving = false;
    private bool isGrowing = false;

    void Start()
    {
        eyes = transform.GetChild(7).GetComponent<Rigidbody2D>();
        Debug.Log(eyes.name);
        // r�cup�r� tout les rigidbody des points
        foreach (Transform child in transform)
        {
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                points.Add(rb);
            }

            // Sauvegarde de la distance d'origine de chaque SpringJoint2D
            foreach (SpringJoint2D joint in child.GetComponents<SpringJoint2D>())
            {
                springDistance[joint] = joint.distance;
            }

        }

        //change all Joint Distance to shrinkFactor:
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
            //lecture de la valeur du joystick/flecheGaucheDroite (playerInputSystem)
            float moveInput = context.ReadValue<Vector2>().x;
            //on applique la valeur du vecteur a la vitesse (pour que -1 et +1 devienne la MaxSpeed)
            //Apr�s on pourra g�rrer la d�c�l�ration et ac�l�ration grace a 'targetVelocity'
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
        if (isMoving)
        {
            //acc�leration
            currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            // d�c�l�ration
            //targetDeceleration = -currentVelocity * 0.5f;
            currentVelocity = Mathf.Lerp(currentVelocity, 0, deceleration * Time.deltaTime);
            //Debug.Log("Target D�c�l�taion " + targetDeceleration);
        }

        Debug.Log("Current Velocity " + currentVelocity);

        newEyesVelocityX = currentVelocity * eyes.velocity.x;

        if (newEyesVelocityX > 4)
        {
            newEyesVelocityX = 4;
        }
        else if (newEyesVelocityX < -4)
        {
            newEyesVelocityX = -4;
        }

        eyes.velocity = new Vector2(newEyesVelocityX, eyes.velocity.y) * Time.deltaTime;
        Debug.Log("Eyes velocity X : " + newEyesVelocityX);
    }

    void FixedUpdate()
    {
        //if (isMoving)
        //{
        //    //acc�leration
        //    currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        //    Debug.Log("Target Velocity " + targetVelocity);
        //}
        //else
        //{
        //    // d�c�l�ration
        //    //targetDeceleration = -currentVelocity * 0.5f;
        //    currentVelocity = Mathf.Lerp(currentVelocity, 0, deceleration * Time.fixedDeltaTime);
        //    //Debug.Log("Target D�c�l�taion " + targetDeceleration);
        //}


        // mettre la vitesse pour tout les point du blop
        //foreach (Rigidbody2D rb in points)
        //{
        //    rb.velocity = new Vector2(currentVelocity, rb.velocity.y);
        //}
    }

    public void GrowUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isGrowing = true;
            Debug.Log("GrowUp");
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

            if (isGrowing)
            {
                joint.distance = originalDistance * factor;
            }
            else
            {
                joint.distance = originalDistance * factor;
            }
        }
    }
}
