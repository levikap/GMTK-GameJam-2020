using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBGMPlayer : ABGMPlayer
{
    [SerializeField]
    private AudioClip song;

    private ALoopedBGM bgmPlayer;

    void Start()
    {
        this.bgmPlayer = this.instantiateBGMPlayer();
        this.bgmPlayer.setSong(song, this.volume, this.secondsUntilLoopStarts);
        this.bgmPlayer.play();
    }
}
