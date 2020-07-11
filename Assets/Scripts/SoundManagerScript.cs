using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip playerDeathSound, playerWalkSound, playerJumpSound, playerCollectSound, awakeSleepSound;
    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerDeathSound = Resources.Load<AudioClip>("FILENAME");
        playerWalkSound = Resources.Load<AudioClip>("FILENAME");
        playerJumpSound = Resources.Load<AudioClip>("FILENAME");
        playerCollectSound = Resources.Load<AudioClip>("FILENAME");
        awakeSleepSound = Resources.Load<AudioClip>("FILENAME");

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public static void PlaySound(string clip)
    //{
    //    switch(clip)
    //    {
    //        case "FILENAME":
    //            audioSource.PlayOneShot(playerDeathSound);
    //            break;
    //        case "FILENAME":
    //            audioSource.PlayOneShot(playerWalkSound);
    //            break;
    //        case "FILENAME":
    //            audioSource.PlayOneShot(playerJumpSound);
    //            break;
    //        case "FILENAME":
    //            audioSource.PlayOneShot(playerCollectSound);
    //            break;
    //        case "FILENAME":
    //            audioSource.PlayOneShot(awakeSleepSound);
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
