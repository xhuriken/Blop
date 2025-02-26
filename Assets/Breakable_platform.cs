using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_platform : MonoBehaviour
{
    [SerializeField] int velocity;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.transform.GetComponentInParent<Rigidbody2D>().velocity.y == velocity)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            breaking();
        }
    }

    void breaking()
    {
        for (int i = 0; i < 5; i++)
        {
            this.gameObject.transform.GetChild(i).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        }
    }
}
