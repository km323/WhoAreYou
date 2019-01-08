using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class SoundVolume
{
    public float bgm = 1.0f;
    public float se = 1.0f;

    public bool mute = false;

    public void Reset()
    {
        bgm = 1.0f;
        se = 1.0f;
        mute = false;
    }
}

public class SoundManager : SingletonMonoBehaviour<SoundManager> {

    public SoundVolume volume = new SoundVolume();
    private AudioClip[] seClips;
    private AudioClip[] bgmClips;

    private Dictionary<string, int> seIndexes = new Dictionary<string, int>();
    private Dictionary<string, int> bgmIndexes = new Dictionary<string, int>();

    const int cNumChannel = 16;

    private AudioSource bgmSource;
    private AudioSource[] seSource = new AudioSource[cNumChannel];


    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
        gameObject.name = "SoundManager";

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        for (int i = 0; i < seSource.Length; i++)
            seSource[i] = gameObject.AddComponent<AudioSource>();

        seClips = Resources.LoadAll<AudioClip>("Audio/SE");
        bgmClips = Resources.LoadAll<AudioClip>("Audio/BGM");

        for (int i = 0; i < seClips.Length; i++)
            seIndexes[seClips[i].name] = i;

        for (int i = 0; i < bgmClips.Length; i++)
            bgmIndexes[bgmClips[i].name] = i;
    }

    public void PlayBgm(string name)
    {
        int index = bgmIndexes[name];
        PlayBgm(index);
    }
    public void PlayBgm(int index)
    {
        if (index < 0 || index >= bgmClips.Length)
            return;

        if (bgmSource.clip == bgmClips[index])
            return;

        bgmSource.Stop();
        bgmSource.clip = bgmClips[index];
        bgmSource.Play();
    }

    public void StopBgm()
    {
        bgmSource.Stop();
    }

    public void PauseBgm()
    {
        bgmSource.Pause();
    }

    public void UnPauseBgm()
    {
        bgmSource.UnPause();
    }

    public void FadeOutBgm(float fadeTime = 1f)
    {
        if (!bgmSource.isPlaying)
            return;

        bgmSource.volume = volume.bgm;
        bgmSource.DOFade(0.0f, fadeTime).SetEase(Ease.InCubic);
    }
    public void FadeInBgm(float fadeTime=1f)
    {
        bgmSource.DOFade(volume.bgm, fadeTime).SetEase(Ease.InCubic);
    }


    public void PlaySe(string name)
    {
        int index = seIndexes[name];
        PlaySe(index);
    }
    public void PlaySe(int index)
    {
        if (index < 0 || index >= bgmClips.Length)
            return;

        foreach(AudioSource source in seSource)
        {
            if(!source.isPlaying)
            {
                source.clip = seClips[index];
                source.Play();
                return;
            }
        }
    }

    public void StopSe()
    {
        foreach(AudioSource source in seSource)
        {
            source.Stop();

        }
    }
}
