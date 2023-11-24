using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public CheckpointManager checkpointManager;
    public PlacementManager placementManager;

    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;

    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject creditsPanel;


    private void Update()
    {
        try
        {
            if (checkpointManager != null)
            {
                if (Keyboard.current.escapeKey.wasPressedThisFrame && !checkpointManager.isFinished)
                {
                    TogglePause();
                }
            }
            else
            {
                if (Keyboard.current.escapeKey.wasPressedThisFrame)
                {
                    TogglePause();
                }
            }
        }
        catch (Exception e){ Debug.Log(e); }
        
    }
    public void LoadCheckpointRace()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadBeginnerRace()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadAdvancedRace()
    {
        SceneManager.LoadScene(5);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);        
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            SFXManager.Instance.PlayAudio("Pause");
            pauseMenu.SetActive(true);
            PauseGame();
        }
        else
        {
            SFXManager.Instance.PlayAudio("Unpause");
            pauseMenu.SetActive(false);
            ResumeGame();
        }
    }

    public void ShowLevelSelect()
    {
        levelPanel.SetActive(true);
    }

    public void HideLevelSelect()
    {
        levelPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
