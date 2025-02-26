using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public CanvasGroup MainPanel;
    public CanvasGroup OptionsPanel;
    public CanvasGroup CreditsPanel;
    public GameObject Transition;

    public void PlayGame()
    {
        Instantiate(Transition, new Vector3(0,0,0), Quaternion.identity);
        StartCoroutine(ChargeFirstLvl());
    }

    private IEnumerator ChargeFirstLvl(){
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Option()
    {
        OptionsPanel.alpha = 1;
        OptionsPanel.blocksRaycasts = true;
        MainPanel.alpha = 0;
        MainPanel.blocksRaycasts = false;
    }

    public void Credits()
    {
        CreditsPanel.alpha = 1;
        CreditsPanel.blocksRaycasts = true;
        MainPanel.alpha = 0;
        MainPanel.blocksRaycasts = false;
    }

    public void Back()
    {
        OptionsPanel.alpha = 0;
        OptionsPanel.blocksRaycasts = false;
        CreditsPanel.alpha = 0;
        CreditsPanel.blocksRaycasts = false;
        MainPanel.alpha = 1;
        MainPanel.blocksRaycasts = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
