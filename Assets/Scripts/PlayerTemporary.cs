using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemporary : MonoBehaviour
{
    public float speed = 5f; // Vitesse du joueur
    public float jumpForce = 7f; // Force du saut
    private Rigidbody2D rb; // Référence au Rigidbody2D du joueur
    public LayerMask groundLayer; // Masque de couche pour le sol
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Récupère le Rigidbody2D au démarrage
    }

    void Update()
    {


        // Récupère l'input horizontal (A/D ou flèches gauche/droite)
        float moveInput = Input.GetAxis("Horizontal");

        // Applique la vitesse au Rigidbody2D
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Détecte le saut avec l'input "Jump" (Espace par défaut)
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    // Méthode de saut
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
