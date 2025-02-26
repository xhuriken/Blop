using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            SceneManager.LoadScene(levelScenes[currentLevelIndex]); 
        }
        else
        {
            Debug.Log("Fin du jeu !");
        }
    }
}
