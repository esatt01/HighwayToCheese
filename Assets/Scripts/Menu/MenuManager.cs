using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;

    public void StartGame()
    {

        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
    }
    public void SettingsQuit()
    {
        settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
