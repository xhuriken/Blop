using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    private lvlManager lvlManager;
    public bool playerBlueInside = false;
    public bool playerRedInside = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BlueBlop"))
        {
            playerBlueInside = true;
        }
        else if (collision.CompareTag("RedBlop"))
        {
            playerRedInside = true;
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
