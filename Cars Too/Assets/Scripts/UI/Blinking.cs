using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{

    private float timer = 1.0f;
    [SerializeField] private Image eyes;
    public bool canblink = true;
    private bool blinking = false;
    public Sprite blinkstate;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(2.0f, 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canblink) {
            return;
        }
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
        Sprite temp = eyes.sprite;
        eyes.sprite = blinkstate;
        float blinktime = 0.0f;
        while (blinktime < 0.25f)
        {
            blinktime += Time.deltaTime;
            yield return null;
        }
        if (eyes.sprite == blinkstate)
        {
            eyes.sprite = temp;
        }
        timer = Random.Range(2.0f, 6.0f);
        blinking = false;
    }
}
