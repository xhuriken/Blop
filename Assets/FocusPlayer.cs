using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPlayer : MonoBehaviour
{
    private GameObject player;
    void Update()
    {
        if(player == null)
        {

        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}
