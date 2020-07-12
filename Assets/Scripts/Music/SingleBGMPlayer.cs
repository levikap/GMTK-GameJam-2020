using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBGMPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject loopedBGMPrefab;
    [SerializeField]
    private AudioClip song;
    [SerializeField]
    private float volume;
    [SerializeField]
    private float secondsUntilLoopStarts;

    private ALoopedBGM bgmPlayer;

    void Start()
    {
        this.bgmPlayer = this.instantiateBGMPlayer();
        this.bgmPlayer.setSong(song, this.volume, this.secondsUntilLoopStarts);
        this.bgmPlayer.play();
    }

    private ALoopedBGM instantiateBGMPlayer()
    {
        GameObject g = Instantiate(this.loopedBGMPrefab);
        g.transform.SetParent(this.transform);
        return g.GetComponent<ALoopedBGM>();
    }

    void Update()
    {
        
    }
}
