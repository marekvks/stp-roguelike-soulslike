using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMainMenu();
        }
    }

    void OpenMainMenu()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMainMenu()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
