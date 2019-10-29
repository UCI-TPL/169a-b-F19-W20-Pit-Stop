using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeStarter : MonoBehaviour
{
    public int index = 0;
    // Sets the levels current theme, and starts playing it
    void Start()
    {
        DataManager.instance.am.SetTheme(index);
        DataManager.instance.am.PlayCurrentTheme();
    }


}
