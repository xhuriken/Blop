using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.parent.GetComponent<Player>() != null)
        {
            collision.gameObject.transform.parent.GetComponent<Player>().Die();
        }
    }
}
