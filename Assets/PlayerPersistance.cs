using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPersistence : MonoBehaviour
{
    public static PlayerPersistence Instance { get; private set; }

    private Dictionary<int, InputDevice> savedDevices = new Dictionary<int, InputDevice>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerDevice(PlayerInput player)
    {
        if (player != null && player.devices.Count > 0)
        {
            savedDevices[player.playerIndex] = player.devices[0];
        }
    }

    public InputDevice GetSavedDevice(int playerIndex)
    {
        return savedDevices.ContainsKey(playerIndex) ? savedDevices[playerIndex] : null;
    }
}
