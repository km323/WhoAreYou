using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum BGM
{
    None=-1,
    Title,
    Game,
    Game2,
}

public enum SE
{
    None = -1,
    DefaultShot,
    Damage,
    ChangeTurn,
    LockOn,
    ShotBegin,
}

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

    private Dictionary<BGM, string> bgmName = new Dictionary<BGM, string>()
    {
        {BGM.Title,"bgmTitle2" },
        {BGM.Game,"Beatmatch info - MT2 03 Synth Cm 129 Bpm" },
        {BGM.Game2,"Beatmatch info - MT2 09 Drums 128 Bpm" }
    };
    private Dictionary<SE, string> seName = new Dictionary<SE, string>()
    {
        {SE.DefaultShot,"cursor7" },
        {SE.Damage,"cancel7" },
        {SE.ChangeTurn,"se_maoudamashii_se_syber01" },
        {SE.LockOn,"warning1" },
        {SE.ShotBegin,"se_maoudamashii_se_pc03" },
    };

    private AudioClip[] seClips;
    private AudioClip[] bgmClips;

    private Dictionary<string, int> seIndexes = new Dictionary<string, int>();
    private Dictionary<string, int> bgmIndexes = new Dictionary<string, int>();

    const int cNumChannel = 16;

    const int bNumChannel = 2;
    //private AudioSource bgmSource;
    private AudioSource[] bgmSource = new AudioSource[bNumChannel];
    private AudioSource[] seSource = new AudioSource[cNumChannel];


    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
        gameObject.name = "SoundManager";

        //bgmSource = gameObject.AddComponent<AudioSource>();
        //bgmSource.loop = true;

        for(int i=0;i<bgmSource.Length;i++)
        {
            bgmSource[i] = gameObject.AddComponent<AudioSource>();
            bgmSource[i].loop = true;
            bgmSource[i].volume = volume.bgm;
        }

        for (int i = 0; i < seSource.Length; i++)
            seSource[i] = gameObject.AddComponent<AudioSource>();

        seClips = Resources.LoadAll<AudioClip>("Audio/SE");
        bgmClips = Resources.LoadAll<AudioClip>("Audio/BGM");

        for (int i = 0; i < seClips.Length; i++)
            seIndexes[seClips[i].name] = i;

        for (int i = 0; i < bgmClips.Length; i++)
            bgmIndexes[bgmClips[i].name] = i;
    }

    public void PlayBgm(BGM id)
    {
        int index= bgmIndexes[bgmName[id]];
        PlayBgm(index);
    }
    public void PlayBgm(int index)
    {
        if (index < 0 || index >= bgmClips.Length)
            return;

        foreach (AudioSource source in bgmSource)
        {
            if (!source.isPlaying)
            {
                source.clip = bgmClips[index];
                source.Play();
                return;
            }
        }
    }

    public void StopBgm()
    {
        foreach (AudioSource source in bgmSource)
        {
            source.Stop();

        }
    }

    //public void PlayBgm(BGM id)
    //{
    //    int index = bgmIndexes[bgmName[id]];
    //    PlayBgm(index);
    //}
    //public void PlayBgm(int index)
    //{
    //    if (index < 0 || index >= bgmClips.Length)
    //        return;

    //    if (bgmSource.clip == bgmClips[index])
    //        return;

    //    bgmSource.Stop();
    //    bgmSource.clip = bgmClips[index];
    //    bgmSource.Play();
    //}

    //public void StopBgm()
    //{
    //    bgmSource.Stop();
    //}

    //public void PauseBgm()
    //{
    //    bgmSource.Pause();
    //}

    //public void UnPauseBgm()
    //{
    //    bgmSource.UnPause();
    //}

    public void FadeOutBgm(int index=1,float fadeTime = 1f)
    {
        if (!bgmSource[index].isPlaying)
            return;

        ///bgmSource[index].volume = volume.bgm;
        bgmSource[index].DOFade(0.0f, fadeTime).SetEase(Ease.InCubic);
    }
    public void FadeInBgm(int index=1, float fadeTime = 1f)
    {
        bgmSource[index].DOFade(volume.bgm, fadeTime).SetEase(Ease.InCubic);
    }
    public void MuteBgm(int index=1)
    {
        bgmSource[index].volume = 0;
    }


    public void PlaySe(SE id)
    {
        int index = seIndexes[seName[id]];
        PlaySe(index);
    }
    public void PlaySe(int index)
    {
        if (index < 0 || index >= seClips.Length)
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
