using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private float timer = 1.0f;
    private float maxtimer = 1.0f;
    [SerializeField] GameObject box = null;

        // Start is called before the first frame update
    void Start()
    {
        maxtimer = timer;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = maxtimer;
            Instantiate(box, this.transform);
        }
    }
}
