using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsUI;
    public GameObject controlsUI;
    public GameObject TitoUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        controlsUI.SetActive(false);
        TitoUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        HideMouseCursor();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        TitoUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        ShowMouseCursor();
    }

    public void QuitGame()
    {
        Debug.Log("QuittingGame");
        Application.Quit();
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
