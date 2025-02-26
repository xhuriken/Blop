using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;
using Febucci.UI;
using System;

public class PlayerInputSpawner: MonoBehaviour
{
    //un used actually

    public GameObject player1Prefab; 
    public GameObject player2Prefab; 
    public Transform spawnPoint1;    
    public Transform spawnPoint2;    
    public TypewriterByCharacter joinType;
    private bool player1Joined = false;
    private bool player2Joined = false;
    private List<InputDevice> usedDevices = new List<InputDevice>();

    private void Awake()
    {
        joinType.ShowText("Press Any Key ...");
    }

    void OnEnable()
    {
        InputSystem.onAnyButtonPress.Call(HandleInput);
    }

    private void HandleInput(InputControl control)
    {
        InputDevice device = control.device;

        if (usedDevices.Contains(device))
            return;

        if (!player1Joined)
        {
            SpawnPlayer(device, player1Prefab, spawnPoint1);
            player1Joined = true;
        }
        else if (!player2Joined) 
        {
            SpawnPlayer(device, player2Prefab, spawnPoint2);
            player2Joined = true;
        }

        if (player1Joined && player2Joined && joinType != null)
        {
            joinType.ShowText("");
        }
    }

    private void SpawnPlayer(InputDevice device, GameObject prefab, Transform spawnPoint)
    {
        GameObject player = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        var playerInput = player.GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            playerInput.SwitchCurrentControlScheme(device);
        }

        usedDevices.Add(device);
    }
}
