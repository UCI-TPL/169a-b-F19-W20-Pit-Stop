using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [SerializeField] private AudioSource a;
   [SerializeField] private AudioSource bgmsource;
   [SerializeField] private AudioSource voices;
    public float bgmvol = 0.0f;
    public float sfxvol = 0.0f;
    public float voicevol = 0.0f;
    //List of all the bgms in the game
   [SerializeField] private List<AudioClip> bgms;
    //the index of the current level's theme
    private int currenttheme;
    private Dictionary<AudioClip, float> bgmtimes = new Dictionary<AudioClip, float>();
    private void Awake()
    {
        bgmvol = bgmsource.volume;
        sfxvol = a.volume;
        voicevol = voices.volume;
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

    public void PlayVL(AudioClip ac)
    {
        if (ac == null)
        {
            voices.Pause();
            return;
        }
        voices.clip = ac;
        voices.Play();
    }

    public void PlayandTrackBGM(int index, bool fade = true, float fadetime =1.25f)
    {
        if (bgmsource.clip != null)
        {
            bgmtimes[bgmsource.clip] = bgmsource.time;
        }
        if (fade)
        {
            StopAllCoroutines();
            StartCoroutine(BGMfade(bgms[index], fadetime));
        }
        else
        {
            PlayBGM(bgms[index], bgmtimes[bgms[index]]);
        }
    }

    private IEnumerator BGMfade(AudioClip a,float fadetime)
    {
        float currentvol = bgmvol;
        Debug.Log("currentvol: "+currentvol);
        float volincrementer = currentvol / (fadetime * 60.0f);
        while (bgmsource.volume > 0)
        {
            
            float temp = bgmsource.volume-volincrementer;
            if (temp <= 0)
            {
                bgmsource.volume = 0;
            }
            else
            {
                bgmsource.volume = temp;
            }
            yield return null;
        }

        PlayBGM(a, bgmtimes[a]);

        while (bgmsource.volume < currentvol)
        {
            float temp = bgmsource.volume + volincrementer;
            if (temp >=currentvol)
            {
                bgmsource.volume = currentvol;
            }
            else
            {
                bgmsource.volume = temp;
            }
            yield return null;
        }
        Debug.Log("finished");
    }
    

    public void SetTheme(int i)
    {
        currenttheme = i;
    }

    public void PlayCurrentTheme()
    {
        PlayandTrackBGM(currenttheme,false);
    }

    public void StopAudioSFX()
    {
        a.Pause();
    }

    public void StopAudioBGM()
    {
        bgmsource.Pause();
    }

    public void setsfxvol(float val)
    {
        a.volume = val;
        sfxvol = val;
    }

    public void setbgmvol(float val)
    {
        bgmsource.volume = val;
        bgmvol = val;
    }
}
