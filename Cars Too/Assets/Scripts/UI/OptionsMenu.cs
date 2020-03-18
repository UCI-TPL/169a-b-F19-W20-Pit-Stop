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
    [SerializeField] Slider vlvol;
    void Start()
    {
        txtspd.value = DataManager.instance.textspeed;
        bgmvol.value = DataManager.instance.am.bgmvol;
        sfxvol.value = DataManager.instance.am.sfxvol;
        vlvol.value = DataManager.instance.am.voicevol;
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
        DataManager.instance.am.setvoicevol(vlvol.value);
    }
}
