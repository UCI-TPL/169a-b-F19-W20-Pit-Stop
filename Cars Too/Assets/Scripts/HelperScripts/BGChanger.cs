using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGChanger : MonoBehaviour
{
    //switches to designated bgm on object becoming awake
    [SerializeField] int songnumber = 5;
    private void Awake()
    {
        DataManager.instance.am.PlayandTrackBGM(songnumber);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
