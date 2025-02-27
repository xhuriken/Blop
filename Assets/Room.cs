using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Room
{
    public Vector3 spawnPoint1;
    public Vector3 spawnPoint2;
    public GameObject vcam;
    public int roomID;
    public Room(Vector3 spawnPoint1, Vector3 spawnPoint2, GameObject vcam,int roomID)
    {
        this.spawnPoint1 = spawnPoint1;
        this.spawnPoint1 = spawnPoint2;
        this.vcam = vcam;
        this.roomID = roomID;
    }
}
