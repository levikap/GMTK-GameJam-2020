using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip playerDeathSound, playerWalkSound, playerJumpSound, collectStarSound, collectCookieSound, awakeSleepSound, levelWonSound,
        transitionToSleepSound, transitionToAwakeSound, transitionWarning;
    public static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerDeathSound = Resources.Load<AudioClip>("Death");
        playerWalkSound = Resources.Load<AudioClip>("Walk");
        playerJumpSound = Resources.Load<AudioClip>("Jump");
        collectStarSound = Resources.Load<AudioClip>("Star");
        collectCookieSound = Resources.Load<AudioClip>("Cookie");
        awakeSleepSound = Resources.Load<AudioClip>("Transition1");
        levelWonSound = Resources.Load<AudioClip>("Win");
        transitionToSleepSound = Resources.Load<AudioClip>("TransitionToSleep");
        transitionToAwakeSound = Resources.Load<AudioClip>("TransitionToAwake");
        transitionWarning = Resources.Load<AudioClip>("TransitionWarning");

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Death":
                audioSource.PlayOneShot(playerDeathSound);
                break;
            case "Jump":
                audioSource.PlayOneShot(playerJumpSound);
                break;
            case "Star":
                audioSource.PlayOneShot(collectStarSound);
                break;
            case "Cookie":
                audioSource.PlayOneShot(collectCookieSound);
                break;
            case "Transition1":
                audioSource.PlayOneShot(awakeSleepSound);
                break;
            case "Win":
                audioSource.PlayOneShot(levelWonSound);
                break;
            case "TransitionToSleep":
                audioSource.PlayOneShot(transitionToSleepSound);
                break;
            case "TransitionToAwake":
                audioSource.PlayOneShot(transitionToAwakeSound);
                break;
            case "TransitionWarning":
                audioSource.PlayOneShot(transitionWarning);
                break;
            default:
                break;
        }
    }
}
