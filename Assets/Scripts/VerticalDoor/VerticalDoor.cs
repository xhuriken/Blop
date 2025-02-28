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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        buttonPressed = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        buttonPressed = false;
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
        button.position = Vector2.Lerp(button.position, new Vector2(buttonInitialPosition.x, buttonInitialPosition.y - buttonPressDepth), 3f * Time.deltaTime);
    }

    void ReleaseButton()
    {
        if (!buttonPressed)
        {
            button.position = Vector2.Lerp(button.position, buttonInitialPosition, 3f * Time.deltaTime);
        }
    }
}