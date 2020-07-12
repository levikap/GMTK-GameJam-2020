﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopedBGM : ALoopedBGM
{
    [SerializeField]
    private GameObject audioSourcePrefab;

    private float baseVolume;
    private float maxVolume;

    private bool transitioning;

    private AudioSource currentAudioSource;
    private AudioSource nextAudioSource;
    private float loopSeconds;

    private float fadeSpeedPerSecond = 0.333f;

    private AudioSource instantiateAudioSource()
    {
        GameObject g = Instantiate(this.audioSourcePrefab);
        g.transform.SetParent(this.transform);
        return g.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (this.currentAudioSource.time >= this.loopSeconds && !this.transitioning)
        {
            this.transitioning = true;
            this.fadeOutSource(this.currentAudioSource);
            this.nextAudioSource.Play();
            this.fadeInSource(this.nextAudioSource);
        }

        if (!this.currentAudioSource.isPlaying && this.transitioning)
        {
            this.transitioning = false;
            AudioSource t = this.currentAudioSource;
            this.currentAudioSource = this.nextAudioSource;
            this.nextAudioSource = t;
        }

        this.capVolume(this.currentAudioSource);
        this.capVolume(this.nextAudioSource);
    }

    private void capVolume(AudioSource source)
    {
        if (source.volume > this.maxVolume)
        {
            source.volume = this.maxVolume;
        }
    }

    private void fadeOutSource(AudioSource source)
    {
        StartCoroutine(this._fadeOutSource(source));
    }

    private IEnumerator _fadeOutSource(AudioSource source)
    {
        while(source.volume > 0)
        {
            source.volume = Mathf.Clamp(source.volume - (this.fadeSpeedPerSecond * Time.deltaTime), 0, this.maxVolume);
            yield return null;
        }
    }

    private void fadeInSource(AudioSource source)
    {
        StartCoroutine(this._fadeInSource(source));
    }

    private IEnumerator _fadeInSource(AudioSource source)
    {
        while (source.volume < this.maxVolume)
        {
            source.volume = Mathf.Clamp(source.volume + (this.fadeSpeedPerSecond * Time.deltaTime), 0, this.maxVolume);
            yield return null;
        }
    }

    public override void fadeIn()
    {
        StartCoroutine(this._fadeIn());
    }

    private IEnumerator _fadeIn()
    {
        while(this.maxVolume < this.baseVolume)
        {
            this.maxVolume = Mathf.Clamp(this.maxVolume + (this.fadeSpeedPerSecond * Time.deltaTime), 0, this.baseVolume);
            yield return null;
        }
    }

    public override void fadeOut()
    {
        StartCoroutine(this._fadeOut());
    }

    private IEnumerator _fadeOut()
    {
        while (this.maxVolume < this.baseVolume)
        {
            this.maxVolume = Mathf.Clamp(this.maxVolume - (this.fadeSpeedPerSecond * Time.deltaTime), 0, this.baseVolume);
            yield return null;
        }
    }

    public override void play()
    {
        this.currentAudioSource.Play();
    }

    public override void stop()
    {
        this.currentAudioSource.Stop();
        this.nextAudioSource.Stop();
    }

    public override void setSong(AudioClip song, float volume, float loopSeconds)
    {
        if (this.currentAudioSource == null || this.nextAudioSource == null)
        {
            this.initSources();
        }
        this.stop();
        this.currentAudioSource.clip = song;
        this.nextAudioSource.clip = song;
        this.baseVolume = volume;
        this.maxVolume = volume;
        this.currentAudioSource.volume = this.baseVolume;
        this.nextAudioSource.volume = 0;
        this.loopSeconds = loopSeconds;
    }

    private void initSources()
    {
        this.currentAudioSource = this.instantiateAudioSource();
        this.nextAudioSource = this.instantiateAudioSource();
    }
}
