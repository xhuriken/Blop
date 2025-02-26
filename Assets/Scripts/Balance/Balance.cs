using UnityEngine;

public class Balance : MonoBehaviour
{
    private Rigidbody2D rb;
    public float inclineForce = 10f; // Force appliquée pour l'inclinaison
    private bool hasWeight = false;
    private Transform playerTransform;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY; // Empêche les déplacements
    }

    void FixedUpdate()
    {
        if (hasWeight && playerTransform != null)
        {
            // Calcul de la distance entre la planche et le joueur sur l'axe X
            float distance = playerTransform.position.x - transform.position.x;
            
            // Appliquer une force en fonction de la position du joueur
            rb.AddForce(new Vector2(distance, 0).normalized * inclineForce * rb.mass);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si la planche touche le sol, elle s'arrête
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.angularVelocity = 0; // Arrêter la rotation
            rb.velocity = Vector2.zero; // Arrêter la translation
        }
    }
}
