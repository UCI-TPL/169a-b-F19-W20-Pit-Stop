using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider txtspd;
    [SerializeField] Slider bgmvol;
    [SerializeField] Slider sfxvol;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void updatevalues()
    {
        DataManager.instance.textspeed = txtspd.value;
        DataManager.instance.am.setsfxvol(sfxvol.value);
        DataManager.instance.am.setbgmvol(bgmvol.value);
    }
}
