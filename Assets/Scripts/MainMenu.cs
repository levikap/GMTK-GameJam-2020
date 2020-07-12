using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioClip buttonClick;
    public AudioSource audioSource;
    public bool creditsOpened = false;

    void Start()
    {
        creditsOpened = false;
        GameObject.FindGameObjectWithTag("CreditsInfo").GetComponent<Image>().enabled = false;
        print(GameObject.FindGameObjectWithTag("CreditsInfo"));
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
        if (creditsOpened)
        {
            GameObject.FindGameObjectWithTag("CreditsInfo").GetComponent<Image>().enabled = false;
        } else
        {
            GameObject.FindGameObjectWithTag("CreditsInfo").GetComponent<Image>().enabled = true;
        }
        audioSource.PlayOneShot(buttonClick);
        creditsOpened = !creditsOpened;
    }

    private void StartGameClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
