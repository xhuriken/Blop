using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayerLvl : MonoBehaviour
{
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    private PlayerInputManager playerInputManager;

    void Start()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();

        if (playerInputManager == null)
        {
            Debug.LogWarning("No PlayerInputManager found, creating a new one.");

            GameObject newPIM = new GameObject("PlayerInputManager");
            playerInputManager = newPIM.AddComponent<PlayerInputManager>();
        }

        for (int i = 0; i < 2; i++)
        {
            InputDevice savedDevice = PlayerPersistence.Instance.GetSavedDevice(i);
            if (savedDevice != null)
            {
                PlayerInput newPlayer = playerInputManager.JoinPlayer(i, -1, null, savedDevice);
                Transform spawnPoint = (i == 0) ? spawnPoint1 : spawnPoint2;
                newPlayer.transform.position = spawnPoint.position;
            }
        }
    }

}
