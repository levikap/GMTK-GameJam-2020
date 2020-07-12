using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBGMPlayer : ABGMPlayer, IDoubleBGMPlayer
{
    [SerializeField]
    private AudioClip songA;
    [SerializeField]
    private AudioClip songB;

    private ALoopedBGM loopA;
    private ALoopedBGM loopB;

    void Start()
    {
        this.loopA = this.instantiateBGMPlayer();
        this.loopA.setSong(this.songA, this.volume, this.secondsUntilLoopStarts);
        this.loopB = this.instantiateBGMPlayer();
        this.loopB.setSong(this.songB, this.volume, this.secondsUntilLoopStarts);

        this.loopA.play();
        this.loopB.play();
        this.loopB.mute();
    }

    public void swapSongs()
    {
        this.loopA.fadeOut();
        this.loopB.fadeIn();

        ALoopedBGM t = this.loopA;
        this.loopA = this.loopB;
        this.loopB = t;
    }
}
