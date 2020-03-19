using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honk : MonoBehaviour
{
    [SerializeField] AudioClip honksound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DataManager.instance.am.PlaySound(honksound);
        }
    }
}
