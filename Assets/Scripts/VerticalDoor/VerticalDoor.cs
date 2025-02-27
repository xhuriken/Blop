using UnityEngine;

public class VerticalDoor : MonoBehaviour
{
    // DOOR
    public Transform door;
    public Transform button;

    public float openDistance = 8f;  
    public float buttonPressDepth = 0.5f; 
    public float speed = 1f; 

    public float requiredDistance = 1f; 
    private Vector2 doorInitialPosition;
    private Vector2 buttonInitialPosition;
    private bool buttonPressed = false; 

    public bool isDoorBlocked = false;

    public Transform[] players; 

    void Start()
    {
        doorInitialPosition = door.position;
        buttonInitialPosition = button.position;
    }

    void Update()
    {
        CheckPlayersOnButton();

        if (buttonPressed)
        {
            OpenDoor();
            PressButton();
        }
        else
        {
            if (!isDoorBlocked)
            {
                CloseDoor();
            }
            ReleaseButton();
        }
    }

    void CheckPlayersOnButton()
    {
        buttonPressed = false;  

 
        foreach (Transform player in players)
        {
            if (Vector2.Distance(player.position, button.position) <= requiredDistance)
            {
                buttonPressed = true; 
                break;
            }
        }
    }

    void OpenDoor()
    {
        door.position = Vector2.Lerp(door.position, new Vector2(door.position.x, doorInitialPosition.y + openDistance), speed * Time.deltaTime);
    }

    void CloseDoor()
    {
        door.position = Vector2.Lerp(door.position, doorInitialPosition, speed * Time.deltaTime);
    }

    void PressButton()
    {
        button.position = Vector2.Lerp(button.position, new Vector2(buttonInitialPosition.x, buttonInitialPosition.y - buttonPressDepth), speed * Time.deltaTime);
    }

    void ReleaseButton()
    {
        if (!buttonPressed)
        {
            button.position = Vector2.Lerp(button.position, buttonInitialPosition, speed * Time.deltaTime);
        }
    }
}
