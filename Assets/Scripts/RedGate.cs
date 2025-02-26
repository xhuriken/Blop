using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGate : MonoBehaviour
{
    private string requiredTag = "RedBlop";
    private void OnTriggerEnter2D(Collider2D other)
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