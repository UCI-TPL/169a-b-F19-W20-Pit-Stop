using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinker : MonoBehaviour
{
    [SerializeField] private float blinktime = 3.0f; //time for each blink
    [SerializeField] private Fadein fi = null;
    [SerializeField] private Fadeout fo = null;
    [SerializeField] private GameObject blinkobj = null;
    [SerializeField] private Image blinkimg = null;
    void Start()
    {
        fi.fadeonawake = false;
        fo.fadeonawake = false;
        fi.timer = blinktime;
        fo.timer = blinktime;
    }

    private void OnEnable()
    {
        blinkobj.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //finished fadeing
        if (!blinkobj.activeSelf)
        {
            blinkobj.SetActive(true);
            StartCoroutine(fi.fade());
        }
        //done fadeing in
        else if (blinkimg.color.a==1.0f)
        {
            StartCoroutine(fo.fade());
        }
    }
}
