using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class CarSounds : MonoBehaviour
{
    [SerializeField] private AudioClip enginesound;
    [SerializeField] private AudioClip enginestart;
    [SerializeField] private AudioSource asource;
    private bool stopped = true;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        asource.volume = DataManager.instance.am.sfxvol;

        if (Mathf.Abs(rb.velocity.magnitude)<=.25)
        {
            stopped = true;
            asource.Pause();
        }
        else if (stopped)
        {
            asource.clip = enginestart;
            asource.loop = false;
            asource.Play();
            stopped = false;
        }
        else if (!asource.isPlaying)
        {
            asource.clip = enginesound;
            asource.loop = true;
            asource.Play();
        }
    }
}
