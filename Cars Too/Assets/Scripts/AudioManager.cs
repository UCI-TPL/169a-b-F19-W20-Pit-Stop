using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [SerializeField] private AudioSource a;
    public void PlaySound(AudioClip ac)
    {
        a.clip = ac;
        a.Play();
    }
}
