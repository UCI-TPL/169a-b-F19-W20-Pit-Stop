using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{

    private ConfidantMenu cm;
    private float timer = 1.0f;
    private Image portrait;
    private bool blinking = false;
    // Start is called before the first frame update
    void Start()
    {
        portrait = GetComponent<Image>();
        cm = GameObject.FindObjectOfType<ConfidantMenu>();
        timer = Random.Range(2.0f, 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!blinking)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StartCoroutine(Blink());
            }
        }
    }

    private IEnumerator Blink()
    {
        blinking = true;
        portrait.sprite = cm.currentNPC.blinkstate;
        float blinktime = 0.0f;
        while (blinktime < 0.25f)
        {
            blinktime += Time.deltaTime;
            yield return null;
        }
        portrait.sprite = cm.currentNPC.consprite;
        timer = Random.Range(2.0f, 6.0f);
        blinking = false;
    }
}
