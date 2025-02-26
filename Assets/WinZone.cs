using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    private lvlManager lvlManager;
    private bool playerBlueInside = false;
    private bool playerRedInside = false;

    public void Start()
    {
        lvlManager = GameObject.FindGameObjectWithTag("lvlManager").GetComponent<lvlManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BlueBlop"))
        {
            playerBlueInside = true;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("RedBlop"))
        {
            playerRedInside = true;
            Destroy(collision.gameObject);
        }

        if (playerBlueInside && playerRedInside)
        {
            lvlManager.NextLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BlueBlop"))
        {
            playerBlueInside = false;
        }
        else if (collision.CompareTag("RedBlop"))
        {
            playerRedInside = false;
        }
    }
}
