using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    public static float time = 0.0f;
    public ParticleSystem glow;

    // Start is called before the first frame update
    void Start()
    {
        glow = GetComponent<ParticleSystem>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        glow.Play();
    }


    private void CheckIfAboutToSwitch()
    {
        if (SwitchController.playParticles)
        {
            glow.Play();
        } else
        {
            glow.Stop();
        }
    }
}
