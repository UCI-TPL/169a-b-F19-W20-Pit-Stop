using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawsEnabler : AbilityEnabler
{
    // Handles the activation of the saws prefab and the instantiation of any values required by it
    public Saws saws;

    //The amount of time that the saws are out for
    public float sawtimer = 5.0f;
    float timer = 0.0f;
    bool running = false;
    void Start()
    {
        saws.gameObject.SetActive(false);
        StartCoroutine(waitForAbil());
    }

    // Update is called once per frame
    void Update()
    {
        if (!running&&Input.GetKeyDown(KeyCode.Q))
        {
            saws.gameObject.SetActive(true);
            running = true;
        }
        else if (running)
        {
            timer += Time.deltaTime;
            if (timer >= sawtimer)
            {
                timer = 0.0f;
                running = false;
                saws.gameObject.SetActive(false);
            }
        }
    }

    //Used to wait for instantiation of the ability
    IEnumerator waitForAbil()
    {
        while (abil == null)
        {
            yield return null;
        }
        saws.abilitydamage = abil.damagemodifier;
    }
}
