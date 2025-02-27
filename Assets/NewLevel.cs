using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevel : MonoBehaviour
{
    public Vector3 SpawnPoint1;
    public Vector3 SpawnPoint2;
    public CinemachineVirtualCamera vcam;
    public bool isLevel = false;
    public NewLevel lastLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BlueBlop") || collision.CompareTag("RedBlop"))
        {
            if (!isLevel)
            {
                lastLevel.isLevel = false;
                isLevel = true;
                GameObject.Find("SpawnPoint1").transform.position = SpawnPoint1;
                GameObject.Find("SpawnPoint2").transform.position = SpawnPoint2;
            }
        }
    }
}
