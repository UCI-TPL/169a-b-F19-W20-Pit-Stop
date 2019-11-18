using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fadein : MonoBehaviour
{
    //will do a fadein for an image and text or just one of either.
    //just the opposite of the fadeout script
    public float timer = 3.0f;
    public TextMeshProUGUI tx = null; //text to be faded out
    public Image im = null; //image to be faded in
    public bool fadeonawake = true; // if true, the fadeout will begin automatically when the script is awake
    //if it is false, the fade coroutine must be called by another script to start

    void Awake()
    {
        if (fadeonawake)
            StartCoroutine(fade());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator fade()
    {
        //start at 0% visible
        float i = 0.0f;
        //calculate total frames active
        float activeframes = timer * 60.0f;
        //calculate how much visibility should increase each frame
        float incrementer = 1 / activeframes;
        while (i <= 1)
        {
            //change transparency of each if they have been assigned
            if (im != null)
                im.color = new Color(im.color.r, im.color.b, im.color.g, i);
            if (tx != null)
                tx.color = new Color(tx.color.r, tx.color.b, tx.color.g, i);
            yield return null;
            i += incrementer;
        }

        //make each component completely visible
        if (im != null)
            im.color = new Color(im.color.r, im.color.b, im.color.g, 1);
        if (tx != null)
            tx.color = new Color(tx.color.r, tx.color.b, tx.color.g, 1);
    }

}
