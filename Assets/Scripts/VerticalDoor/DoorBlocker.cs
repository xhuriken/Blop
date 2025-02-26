using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlocker : MonoBehaviour
{

    public VerticalDoor door;


    void OnCollisionEnter2D(Collision2D collision)
    {
        door.isDoorBlocked = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        door.isDoorBlocked = false;
    }
}
