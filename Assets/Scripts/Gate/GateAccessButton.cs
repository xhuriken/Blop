using System.Collections;
using UnityEngine;

public class GateAccessButton : MonoBehaviour
{

    public Color redColor = Color.red;
    public Color blueColor = Color.blue;
    public Gate gate;
    public Renderer buttonRenderer;

    public float cooldownTime = 3f;
    public KeyCode activationKey = KeyCode.E;

    private bool isCooldown = false;

    //Nan fin sah mec pourquoi ? tu fait un bool genre ?
    private Collider2D playerInRange;

    private void Start()
    {
        UpdateButtonColor();
    }

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
        if (other.gameObject.transform.parent.CompareTag("RedBlop") || other.gameObject.transform.parent.CompareTag("BlueBlop"))
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
        UpdateButtonColor();

        StartCoroutine(Cooldown());
    }

    private void UpdateButtonColor()
    {
        if (gate.requiredTag == gate.tagOne)
        {
            buttonRenderer.material.color = redColor;
        }
        else
        {
            buttonRenderer.material.color = blueColor;
        }
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}
