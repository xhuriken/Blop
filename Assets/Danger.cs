using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    public Vector2 direction;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.parent.GetComponent<Player>() != null)
        {
            if(collision.gameObject.transform.parent.GetComponent<Player>().isDead == false && !GameManager.Instance.isPlayerDead)
            {
                Debug.Log("___Player Tuch");
                collision.gameObject.transform.parent.GetComponent<Player>().Die();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
            }
        }
    }
}
