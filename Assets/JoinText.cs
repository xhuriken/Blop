using Febucci.UI;
using UnityEngine;
using UnityEngine.InputSystem;

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
        Debug.Log("Player joined: " + player.playerIndex);
        int playerIndex = playerInputManager.playerCount - 1;


        Transform tr = player.transform;
  
         tr.position = menuSpawnPoints[playerIndex].transform.position;

        if (playerInputManager.playerCount >= 2)
        {
            HideText("");
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
