using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenu;
    public GameObject bg;

    public AudioClip buttonClick;
    public AudioSource audioSource;

    private void Start()
    {
        buttonClick = Resources.Load<AudioClip>("Click");
        audioSource = GetComponent<AudioSource>();
        isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameState.isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public void PauseGame()
    {
        audioSource.PlayOneShot(buttonClick);
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PauseGameForLoadingScreen()
    {
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        audioSource.PlayOneShot(buttonClick);
        isGamePaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMainMenu()
    {
        audioSource.PlayOneShot(buttonClick);
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void ExitGame()
    {
        audioSource.PlayOneShot(buttonClick);
        Application.Quit();
    }
}
