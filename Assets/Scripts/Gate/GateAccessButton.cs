using System.Collections;
using UnityEngine;

public class GateAccessButton : MonoBehaviour
{
    public Color redColor = Color.red;
    public Color blueColor = Color.blue;
    public Gate gate;
    public Renderer buttonRenderer;
    public float cooldownTime = 3f;
    public float pressDepth = 0.2f;
    public float pressSpeed = 0.1f; 

    private bool isCooldown = false;
    private Vector3 initialPosition;
    private Coroutine pressCoroutine;

    private void Start()
    {
        initialPosition = transform.position;
        UpdateButtonColor();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCooldown && (collision.gameObject.CompareTag("RedBlop") || collision.gameObject.CompareTag("BlueBlop")))
        {
            Rigidbody2D rb = collision.rigidbody;

            // Vérifie si le joueur descend avant l'impact
            if (rb.velocity.y < 0 && collision.contacts[0].point.y > transform.position.y)
            {
                if (pressCoroutine == null)
                {
                    pressCoroutine = StartCoroutine(PressButton());
                }
            }
        }
    }

    private IEnumerator PressButton()
    {
        isCooldown = true;
        Vector3 pressedPosition = initialPosition - new Vector3(0, pressDepth, 0);

        // Descente du bouton
        float elapsedTime = 0;
        while (elapsedTime < pressSpeed)
        {
            transform.position = Vector3.Lerp(initialPosition, pressedPosition, elapsedTime / pressSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = pressedPosition;

        ActivateButton();

        // Attente de la fin du cooldown
        yield return new WaitForSeconds(cooldownTime);

        // Remontée du bouton
        elapsedTime = 0;
        while (elapsedTime < pressSpeed)
        {
            transform.position = Vector3.Lerp(pressedPosition, initialPosition, elapsedTime / pressSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = initialPosition;

        isCooldown = false;
        pressCoroutine = null;
    }

    private void ActivateButton()
    {
        gate.ToggleGateTag();
        UpdateButtonColor();
    }

    private void UpdateButtonColor()
    {
        buttonRenderer.material.color = (gate.requiredTag == gate.tagOne) ? redColor : blueColor;
    }
}
