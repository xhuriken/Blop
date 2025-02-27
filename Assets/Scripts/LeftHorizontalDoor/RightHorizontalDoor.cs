using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHorizontalDoor : MonoBehaviour
{
    public Transform door;
    public Transform button;
    public float openDistance = 8f;
    public float buttonPressDepth = 0.75f;
    public float speed = 2f;
    public float requiredDistance = 1f;
    public float buttonPressSpeed = 0.1f;
    public float cooldownTime = 1f;

    private Vector2 initialDoorPosition;
    private Vector2 initialButtonPosition;
    private bool doorOpen = false;
    private bool buttonPressed = false;
    private float lerpTime = 0f;
    private Coroutine pressCoroutine;

    public Transform[] players;

    void Start()
    {
        initialDoorPosition = door.position;
        initialButtonPosition = button.position;
    }

    void Update()
    {
        CheckPlayersInRange();
        MoveDoor();
    }

    void CheckPlayersInRange()
    {
        foreach (Transform player in players)
        {
            if (Vector2.Distance(player.position, button.position) <= requiredDistance && !buttonPressed)
            {
                if (pressCoroutine == null)
                {
                    pressCoroutine = StartCoroutine(PressButton());
                }
                return;
            }
        }
    }

    IEnumerator PressButton()
    {
        buttonPressed = true;
        Vector3 pressedPosition = initialButtonPosition - new Vector2(0, buttonPressDepth);
        float elapsedTime = 0;

        while (elapsedTime < buttonPressSpeed)
        {
            button.position = Vector2.Lerp(button.position, pressedPosition, elapsedTime / buttonPressSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        button.position = pressedPosition;

        ToggleDoor();

        yield return new WaitForSeconds(cooldownTime);

        elapsedTime = 0;
        while (elapsedTime < buttonPressSpeed)
        {
            button.position = Vector2.Lerp(button.position, initialButtonPosition, elapsedTime / buttonPressSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        button.position = initialButtonPosition;

        buttonPressed = false;
        pressCoroutine = null;
    }

    void ToggleDoor()
    {
        doorOpen = !doorOpen;
        lerpTime = 0f;
    }

    void MoveDoor()
    {
        if (lerpTime < 1f)
        {
            lerpTime += Time.deltaTime * speed;
            float targetX = doorOpen ? initialDoorPosition.x + openDistance : initialDoorPosition.x;
            door.position = new Vector2(Mathf.Lerp(door.position.x, targetX, lerpTime), initialDoorPosition.y);
        }
    }
}