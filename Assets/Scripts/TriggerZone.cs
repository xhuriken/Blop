using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public ShowSign tutoSign;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BlueBlop") && tutoSign != null || other.CompareTag("RedBlop") && tutoSign != null)
        {
            tutoSign.SetActiveState(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BlueBlop") && tutoSign != null || other.CompareTag("RedBlop") && tutoSign != null)
        {
            tutoSign.SetActiveState(false);
        }
    }
}
