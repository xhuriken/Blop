using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerLvl : MonoBehaviour
{ 
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    void Start()
    {
        GameObject.FindGameObjectWithTag("BlueBlop").transform.position = spawnPoint1.transform.position;
        GameObject.FindGameObjectWithTag("RedBlop").transform.position = spawnPoint2.transform.position;
    }
}
