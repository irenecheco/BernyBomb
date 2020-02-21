using System;
using UnityEngine.Audio;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    bool _isPlaying = false;
    
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMix;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (!_isPlaying)
        {
            StartCoroutine(playSound());
        }

        IEnumerator playSound()
        {
            _isPlaying = true;
            s.source.Play();
            yield return new WaitForSeconds(s.clip.length);
            _isPlaying = false;
        }
    }

    public void Fade(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.DOFade(0.2f, 2.0f);
    }

    public void Fade2(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.DOFade(0.0f, 2.0f);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
        _isPlaying = false;
    }
}
