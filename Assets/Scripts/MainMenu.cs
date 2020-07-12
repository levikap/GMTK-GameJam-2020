using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip buttonClick;
    public AudioSource audioSource;

    void Start()
    {
        buttonClick = Resources.Load<AudioClip>("Click");
        audioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        audioSource.PlayOneShot(buttonClick);
        Invoke("StartGameClicked", 1f);
    }

    public void ExitGame()
    {
        audioSource.PlayOneShot(buttonClick);
        Invoke("QuitGame", 0.5f);
    }


    public void CreditsGame()
    {
        audioSource.PlayOneShot(buttonClick);
    }

    private void StartGameClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
