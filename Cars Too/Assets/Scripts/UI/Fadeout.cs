using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Fadeout : MonoBehaviour
{
    public float timer = 3.0f;
    public TextMeshProUGUI tx = null;
    public Image im;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fade());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator fade()
    {

        float i = 1.0f;
        float activeframes = 180.0f;
        float incrementer = i / activeframes;
        while (i > 0)
        {
            Debug.Log("gere");
            im.color = new Color(im.color.r, im.color.b, im.color.g, i);
            tx.color = new Color(tx.color.r, tx.color.b, tx.color.g, i);
            yield return null;
            i -= incrementer;
        }
      tx.gameObject.SetActive(false);
    }

}
