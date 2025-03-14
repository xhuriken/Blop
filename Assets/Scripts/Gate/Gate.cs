using UnityEngine;

public class Gate : MonoBehaviour
{
    // Les particules associées à la porte
    public ParticleSystem redParticles;
    public ParticleSystem blueParticles;
    
    // Les tags autorisés
    public string tagOne = "RedBlop";
    public string tagTwo = "BlueBlop";

    // Tag requis actuellement (modifiable)
    public string requiredTag;

    private Renderer gateRenderer;

    private void Start()
    {
        gateRenderer = GetComponent<Renderer>();

        // Initialise la Gate avec la couleur correspondant au tag autorisé
        SetGateTag(requiredTag);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag(tagOne) || other.CompareTag(tagTwo))
        {
            // Si le tag ne correspond pas au tag requis, le joueur est tué
            if (!other.CompareTag(requiredTag))
            {
                KillPlayer(other.gameObject);  // Tu peux ici spécifier plus précisément le type de joueur que tu veux "tuer"
            }
        }
    }

    private void KillPlayer(GameObject player)
    {
        Debug.Log("Le joueur est tué.");
    }

    // Méthode pour alterner le tag et la couleur
    public void ToggleGateTag()
    {
        // Alterne entre les deux tags
        requiredTag = (requiredTag == tagOne) ? tagTwo : tagOne;

        // Met à jour la couleur en fonction du tag
        SetGateTag(requiredTag);
    }

    private void SetGateTag(string tag)
    {
        // Change la couleur de la Gate en fonction du tag
        if (tag == tagOne)
        {
            redParticles.Play();
            blueParticles.Stop();
        }
        else if (tag == tagTwo)
        {

            blueParticles.Play();
            redParticles.Stop();
        }
    }
}
