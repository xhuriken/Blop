using UnityEngine;

public class BlopPoint : MonoBehaviour
{
    private bool isGrounded;
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
