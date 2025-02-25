using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAccessButton : MonoBehaviour
{
    public GameObject gate; // Assignez l'objet Gate dans l'Inspector
    public string tagOne = "RedBlop"; // Premier tag requis
    public string tagTwo = "BlueBlop"; // Deuxième tag requis
    public float cooldownTime = 2f; // Temps de cooldown en secondes
    private bool useTagOne = true; // Indique quel tag utiliser
    private bool isCooldown = false; // Indique si le bouton est en cooldown
    private Gate gateScript;

    void Start()
    {
        if (gate != null)
        {
            gateScript = gate.GetComponent<Gate>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RedBlop"))
        {
            if (gateScript != null)
            {
                if (useTagOne)
                {
                    gateScript.SetRequiredTag(tagOne);
                    Debug.Log("Tag requis pour la Gate changé en: " + tagOne);
                }
                else
                {
                    gateScript.SetRequiredTag(tagTwo);
                    Debug.Log("Tag requis pour la Gate changé en: " + tagTwo);
                }

                // Alterne le tag requis pour la prochaine fois
                useTagOne = !useTagOne;
                
                StartCoroutine(Cooldown());
            }
        }
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}
