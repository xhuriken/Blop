using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public CanvasGroup OptionPanel;
    public CanvasGroup MainMenuPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Option()
    {
        OptionPanel.alpha = 1;
        OptionPanel.blocksRaycasts = true;
        MainMenuPanel.alpha = 0;
        MainMenuPanel.blocksRaycasts = false;
    }

    public void Back()
    {
        OptionPanel.alpha = 0;
        OptionPanel.blocksRaycasts = false;
        MainMenuPanel.alpha = 1;
        MainMenuPanel.blocksRaycasts = true;
    }



    public void QuitGame()
    {
        Application.Quit();
    }
}
