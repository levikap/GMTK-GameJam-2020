using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALoopedBGM : MonoBehaviour
{
    public abstract void play();

    public abstract void stop();

    public abstract void fadeIn();

    public abstract void fadeOut();

    public abstract void setSong(AudioClip song, float volume, float loopSeconds);
}
