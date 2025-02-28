using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_platform : MonoBehaviour
{
    [SerializeField] float velocity;
    [SerializeField] AudioSource audioSource;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.GetComponentInParent<Rigidbody2D>().velocity.y < velocity && collision.transform.GetComponentInParent<Player>().isGrowing == false)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            breaking();
        }
    }

    void breaking()
    {
        audioSource.Play();
        for (int i = 0; i < 5; i++)
        {
            this.gameObject.transform.GetChild(i).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(FadeOutObject(this.gameObject.transform.GetChild(i)));

        }
    }

    public IEnumerator FadeOutObject(Transform square)
    {
        while (square.GetComponent<SpriteRenderer>().material.color.a > 0)
        {
            Color objectColor = square.GetComponent<SpriteRenderer>().material.color;
            float fadeAmount = objectColor.a - (0.3f * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            square.GetComponent<SpriteRenderer>().material.color = objectColor;
            if(square.GetComponent<SpriteRenderer>().material.color.a <= 0)
            {
                Destroy(this.gameObject);
            }
            yield return null;
        }

    }

}
