using UnityEngine;

public class BlopPoint : MonoBehaviour
{
    private bool isGrounded;
    private Player player;

    private void Awake()
    {
        // On suppose que ce script est attaché à un enfant du "Player".
        // On va donc chercher le Player dans le parent.
        player = GetComponentInParent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si l'objet que je touche a le tag "Ground", on considère que ce point est au sol.
        if (collision.collider.CompareTag("Ground"))
        {
            if (!isGrounded)
            {
                isGrounded = true;
                player.NotifyPointGrounded();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            if (isGrounded)
            {
                isGrounded = false;
                player.NotifyPointUngrounded();
            }
        }
    }
}
