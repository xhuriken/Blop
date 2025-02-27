using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHorizontalDoor : MonoBehaviour
{
    public Transform door;  
    public Transform button;
    public float openDistance = 8f; 
    public float buttonPressDepth = 0.75f;
    public float speed = 1f; 

    private Vector2 initialDoorPosition; 
    private Vector2 initialButtonPosition; 
    private bool doorOpened = false; 
    private float lerpTime = 0f; 
    private bool playerInRange = false;

    void Start()
    {
        initialDoorPosition = door.position; 
        initialButtonPosition = button.position;
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
            PressButton();
        }
    }

    void OpenDoor()
    {
        lerpTime += Time.deltaTime * speed;

        float targetX = initialDoorPosition.x + openDistance;

        door.position = Vector2.Lerp(initialDoorPosition, new Vector2(targetX, initialDoorPosition.y), lerpTime);

        if (lerpTime >= 1f)
        {
            door.position = new Vector2(targetX, initialDoorPosition.y);
            doorOpened = false;
        }
    }

    void PressButton()
    {
        button.position = Vector2.Lerp(button.position, new Vector2(initialButtonPosition.x, initialButtonPosition.y - buttonPressDepth), speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.parent.CompareTag("BlueBlop") || other.gameObject.transform.parent.CompareTag("RedBlop"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.transform.parent.CompareTag("BlueBlop") || other.gameObject.transform.parent.CompareTag("RedBlop"))
        {
            playerInRange = false;
        }
    }
}
