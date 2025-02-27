using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public Vector3 SpawnPoint1;
    public Vector3 SpawnPoint2;
    public CinemachineVirtualCamera vcam;

    public Level(Vector3 spawnPoint1, Vector3 spawnPoint2, CinemachineVirtualCamera vcam)
    {
        this.SpawnPoint1 = spawnPoint1;
        this.SpawnPoint2 = spawnPoint2;
        this.vcam = vcam;
    }
}
