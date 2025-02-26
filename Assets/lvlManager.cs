using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class lvlManager : MonoBehaviour
{
    public List<string> levelScenes;
    private int currentLevelIndex = 0;


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void NextLevel()
    {
        if (currentLevelIndex + 1 < levelScenes.Count) 
        {
            currentLevelIndex++;
            foreach (var player in FindObjectsOfType<PlayerInput>())
            {
                PlayerPersistence.Instance.SavePlayerDevice(player);
                Debug.Log("Saved player device");
                Destroy(player.gameObject);
            }
            SceneManager.LoadScene(levelScenes[currentLevelIndex]); 
        }
        else
        {
            Debug.Log("Fin du jeu !");
        }
    }
}
