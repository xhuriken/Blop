using Febucci.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class JoinText : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    public TypewriterByCharacter typewriter;
    [SerializeField] Transform[] menuSpawnPoints;

    void Start()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();

        if (playerInputManager == null)
        {
            Debug.LogError("PlayerInputManager not found in the scene!");
            return;
        }

        ShowText("Press Any Key ...");

        playerInputManager.onPlayerJoined += OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        if(SceneManager.GetActiveScene().name == "lvl1")
        {
            Debug.Log("Player joined: " + player.playerIndex);

            if (menuSpawnPoints == null || menuSpawnPoints.Length == 0)
            {
                GameObject spawn1 = GameObject.Find("SpawnPoint1");
                GameObject spawn2 = GameObject.Find("SpawnPoint2");

                if (spawn1 != null && spawn2 != null)
                {
                    menuSpawnPoints = new Transform[] { spawn1.transform, spawn2.transform };
                    Debug.Log("Spawn points auto-assigned.");
                }
                else
                {
                    Debug.LogError("Spawn points not found in the scene! Make sure SpawnPoint1 and SpawnPoint2 exist.");
                    return;
                }
            }

            int playerIndex = playerInputManager.playerCount - 1;
        
            player.transform.position = menuSpawnPoints[playerIndex].position;


            if (playerInputManager.playerCount >= 2)
            {
                HideText("");
            }
        }
    }

    private void ShowText(string message)
    {
        typewriter.ShowText(message);
    }

    private void HideText(string message)
    {
        typewriter.ShowText(message);
    }

    private void OnDestroy()
    {
        if (playerInputManager != null)
        {
            playerInputManager.onPlayerJoined -= OnPlayerJoined;
        }
    }
}
