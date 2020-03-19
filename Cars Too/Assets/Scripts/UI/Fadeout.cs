using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Fadeout : MonoBehaviour
{
    //will do a fadeout for an image and text or just one of either.
    public float timer = 3.0f;
    public TextMeshProUGUI tx = null; //text to be faded out
    public Image im=null; //image to be faded in
    public bool fadeonawake = true; // if true, the fadeout will begin automatically when the script is awake
    //if it is false, the fade coroutine must be called by another script to start
    public bool donefadeing = false;

    void Awake()
    {
        if(fadeonawake)
            StartCoroutine(fade());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator fade()
    {
        donefadeing = false;
        //start at 100% visible
        float i = timer;
        //calculate total frames active
        float activeframes = timer;
        //calculate how much visibility should decrease each frame
        float incrementer = i / activeframes;
        while (i >= 0)
        {
            //change transparency of each if they have been assigned
            if (im != null) 
                im.color = new Color(im.color.r, im.color.b, im.color.g, i);
            if(tx!=null)
                tx.color = new Color(tx.color.r, tx.color.b, tx.color.g, i);
            yield return null;
            i -= Time.deltaTime;
        }
        if (tx != null) { 
            tx.gameObject.SetActive(false);
            tx.color = new Color(tx.color.r, tx.color.b, tx.color.g, 0.0f);
        }
        if (im != null)
        {
            im.gameObject.SetActive(false);
            im.color = new Color(im.color.r, im.color.b, im.color.g, 0.0f);
        }
        donefadeing = true;
    }

}
