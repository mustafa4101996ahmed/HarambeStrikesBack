using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject loadingPanel;


    void Start ()
    {
        loadingPanel.SetActive(false);
    }

    public void OpenControls ()
    {
        SceneManager.LoadScene("MainMenuControls");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}



