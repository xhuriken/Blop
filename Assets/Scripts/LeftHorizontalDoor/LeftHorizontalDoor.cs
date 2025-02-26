using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHorizontalDoor : MonoBehaviour
{
    public Transform door;  
    public float openDistance = 8f; 
    public float speed = 1f; 

    private Vector2 initialPosition; 
    private bool doorOpened = false; 
    private float lerpTime = 0f; 
    private bool playerInRange = false;


    void Start()
    {
        initialPosition = door.position; 
    }

    void Update()
    {
        if (playerInRange && !doorOpened && Input.GetKeyDown(KeyCode.E))
        {
            doorOpened = true; 
        }

        if (doorOpened)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        lerpTime += Time.deltaTime * speed;

        float targetX = initialPosition.x - openDistance;

        door.position = Vector2.Lerp(initialPosition, new Vector2(targetX, initialPosition.y), lerpTime);

        if (lerpTime >= 1f)
        {
            door.position = new Vector2(targetX, initialPosition.y);
            doorOpened = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BlueBlop") || other.CompareTag("RedBlop"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BlueBlop") || other.CompareTag("RedBlop"))
        {
            playerInRange = false;
        }
    }
}
