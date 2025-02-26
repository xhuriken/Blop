using UnityEngine;

public class VerticalDoor : MonoBehaviour
{
    public Transform door;  
    public float openDistance = 8f;  
    public float speed = 1f;  

    private Vector2 initialPosition; 
    private bool buttonPressed = false;  

    public bool isDoorBlocked = false;
    

    void Start()
    {
        
        initialPosition = door.position;
    }

    void Update()
    {
        
        if (buttonPressed)
        {
            
            OpenDoor();
        }
        else
        {
           if (!isDoorBlocked) 
           {
                CloseDoor();
           }     
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        buttonPressed = true; 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        buttonPressed = false; 

    }

    void OpenDoor()
    {
        
        door.position = Vector2.Lerp(door.position, new Vector2(door.position.x, initialPosition.y + openDistance), speed * Time.deltaTime);
    }

    void CloseDoor()
    {
        door.position = Vector2.Lerp(door.position, initialPosition, speed * Time.deltaTime);
    }
}
