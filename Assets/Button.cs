using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;

public class Button : MonoBehaviour
{

    [SerializeField] private GameObject button;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("button"))
        {
            Debug.Log("testbutton");
            button.transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
