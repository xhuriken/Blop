using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform topPosition;  
    public Transform bottomPosition; 
    public float speed = 1f;

    private bool isMovingUp = false;
    private bool isMovingDown = false;

    private int blueBlopCount = 0;
    private int redBlopCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BlueBlop"))
        {
            blueBlopCount++;
        }
        else if (other.CompareTag("RedBlop"))
        {
            redBlopCount++;
        }

        if (blueBlopCount > 0 && redBlopCount > 0) 
        {
            isMovingUp = true;
            isMovingDown = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BlueBlop"))
        {
            blueBlopCount = Mathf.Max(0, blueBlopCount - 1);
        }
        else if (other.CompareTag("RedBlop"))
        {
            redBlopCount = Mathf.Max(0, redBlopCount - 1);
        }

        if (blueBlopCount == 0 || redBlopCount == 0) 
        {
            isMovingUp = false;
            isMovingDown = true;
        }
    }

    void Update()
    {
        if (isMovingUp)
        {
            transform.position = Vector2.MoveTowards(transform.position, topPosition.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, topPosition.position) < 0.1f)
            {
                isMovingUp = false; 
            }
        }
        else if (isMovingDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, bottomPosition.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, bottomPosition.position) < 0.1f)
            {
                isMovingDown = false;
            }
        }
    }
}
