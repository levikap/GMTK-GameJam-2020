using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABGMPlayer : MonoBehaviour
{
    [SerializeField]
    protected GameObject loopedBGMPrefab;
    [SerializeField]
    protected float volume;
    [SerializeField]
    protected float secondsUntilLoopStarts;

    protected ALoopedBGM instantiateBGMPlayer()
    {
        GameObject g = Instantiate(this.loopedBGMPrefab);
        g.transform.SetParent(this.transform);
        return g.GetComponent<ALoopedBGM>();
    }
}
