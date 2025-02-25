using System.Collections;
using UnityEngine;

public class GateAccessButton : MonoBehaviour
{
    public Gate gate; // Assurez-vous que la référence à Gate est bien assignée dans l'Inspector
    public float cooldownTime = 0.5f;
    public KeyCode activationKey = KeyCode.E;

    private bool isCooldown = false;
    private Collider2D playerInRange;

    private void Update()
    {
        // Vérifie si le joueur est proche et appuie sur le bouton
        if (playerInRange && Input.GetKeyDown(activationKey) && !isCooldown)
        {
            ActivateButton();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Détecte si un Blop est proche
        if (other.CompareTag("RedBlop") || other.CompareTag("BlueBlop"))
        {
            playerInRange = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerInRange == other)
        {
            playerInRange = null;
        }
    }

    private void ActivateButton()
    {
        // Change le tag autorisé et la couleur de la Gate
        gate.ToggleGateTag();
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}
