using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private List<Rigidbody2D> points = new List<Rigidbody2D>(); // liste des points du blop (on gère que les RB)
    private Dictionary<SpringJoint2D, float> originalDistances = new Dictionary<SpringJoint2D, float>();

    public float maxSpeed = 10.0f;
    public float acceleration = 5.0f; 
    public float deceleration = 5.0f; 
    public float growthFactor = 1.2f;
    public float shrinkFactor = 0.8f;

    private float targetVelocity = 0f;
    private float currentVelocity = 0f;
    private bool isMoving = false;
    private bool isGrowing = false;

    void Start()
    {
        // récupéré tout les rigidbody des points
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
                originalDistances[joint] = joint.distance;
            }
        }

        //change all Joint Distance to shrinkFactor:
        foreach (var segment in originalDistances)
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
            //Après on pourra gérrer la décélération et acélération grace a 'targetVelocity'
            targetVelocity = moveInput * maxSpeed;
            
            isMoving = true;
        }
        else if (context.canceled)
        {
            isMoving = false;
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            //accéleration
            currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // décélération
            currentVelocity = Mathf.Lerp(currentVelocity, 0, deceleration * Time.fixedDeltaTime);
        }

        // mettre la vitesse pour tout les point du blop
        foreach (Rigidbody2D rb in points)
        {
            rb.velocity = new Vector2(currentVelocity, rb.velocity.y);
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
        foreach (var segment in originalDistances)
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
