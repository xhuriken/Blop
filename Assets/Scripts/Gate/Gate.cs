using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public string requiredTag = "RedBlop"; // Le tag requis initial

    public void SetRequiredTag(string newTag)
    {
        requiredTag = newTag;
        Debug.Log("Nouveau tag requis pour passer la Gate: " + requiredTag);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(requiredTag))
        {
            KillPlayer(other.gameObject);
        }
    }

    private void KillPlayer(GameObject player)
    {
        Debug.Log("Le joueur est tué.");
    }
}
