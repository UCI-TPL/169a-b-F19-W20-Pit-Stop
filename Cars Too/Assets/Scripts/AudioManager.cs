using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [SerializeField] private AudioSource a;
   [SerializeField] private AudioSource bgmsource;
    //List of all the bgms in the game
   [SerializeField] private List<AudioClip> bgms;
    //the index of the current level's theme
    private int currenttheme;
    private Dictionary<AudioClip, float> bgmtimes = new Dictionary<AudioClip, float>();
    private void Awake()
    {
        foreach (AudioClip ac in bgms)
        {
            bgmtimes[ac] = 0.0f;
        }
        bgmsource.clip = null;

    }
    public void PlaySound(AudioClip ac)
    {
        a.clip = ac;
        a.Play();
    }
    
    //plays a bgm without tracking its place in the song
    public void PlayBGM(AudioClip ac, float time =0.0f)
    {
        bgmsource.clip = ac;
        bgmsource.time = time;
        bgmsource.Play();
    }

    public void PlayandTrackBGM(int index)
    {
        if (bgmsource.clip != null)
        {
            bgmtimes[bgmsource.clip] = bgmsource.time;
        }

        PlayBGM(bgms[index], bgmtimes[bgms[index]]);
    }

    public void SetTheme(int i)
    {
        currenttheme = i;
    }

    public void PlayCurrentTheme()
    {
        PlayandTrackBGM(currenttheme);
    }

    public void StopAudioSFX()
    {
        a.Pause();
    }

    public void StopAudioBGM()
    {
        bgmsource.Pause();
    }
}
