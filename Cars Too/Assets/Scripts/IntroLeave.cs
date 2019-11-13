using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLeave : MonoBehaviour
{
    [SerializeField] private float timer = 10.0f; // time before leaving
    [SerializeField] private string lvlname = "LVL1Redesign";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Leave();
        }
    }

    private void Leave()
    {
        DataManager.instance.Reset();
        SceneManager.LoadScene(lvlname);
    }
}
