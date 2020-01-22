using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void Save()
    {
        SaveData sd = new SaveData
        {
            phase = DataManager.instance.phase,

            piperexp = DataManager.instance.confidantExp["Piper"].affinity,
            springexp = DataManager.instance.confidantExp["Springtrap"].affinity,
            dexexp = DataManager.instance.confidantExp["Dex"].affinity,
            mustangexp = DataManager.instance.confidantExp["Mustang"].affinity,

            gift1 = DataManager.instance.GetGiftCount(PresentType.one),
            gift2 = DataManager.instance.GetGiftCount(PresentType.two),
            gift3 = DataManager.instance.GetGiftCount(PresentType.four),
            gift4 = DataManager.instance.GetGiftCount(PresentType.five),

            canDestroy = DataManager.instance.canDestroy,
            canBoost = DataManager.instance.canBoost,
            affinityBoost = DataManager.instance.affinityBoost,
            canHack = DataManager.instance.canHack,
            canThrow = DataManager.instance.canThrow,
            piperExamine = DataManager.instance.piperExamine,

            carparts = DataManager.instance.carParts,
            partsneeded = DataManager.instance.partsneeded,

            textspeed = DataManager.instance.textspeed,
            bgmvolume = DataManager.instance.am.bgmvol,
            sfxvolume = DataManager.instance.am.sfxvol,
            playername = DataManager.instance.GetName(),

            currentscene = DataManager.instance.scenename,

        };
        string json =JsonUtility.ToJson(sd);
        Debug.Log(json);
        File.WriteAllText(Application.dataPath + "/WoWsave.txt", json);


    }

    public void Load()
    {
        string json = File.ReadAllText(Application.dataPath + "/WoWsave.txt");
        SaveData sd = JsonUtility.FromJson<SaveData>(json);
        DataManager.instance.phase = sd.phase;

        DataManager.instance.confidantExp["Piper"].affinity = sd.piperexp;
        DataManager.instance.confidantExp["Springtrap"].affinity = sd.springexp;
        DataManager.instance.confidantExp["Dex"].affinity = sd.dexexp;
        DataManager.instance.confidantExp["Mustang"].affinity = sd.mustangexp;

        DataManager.instance.gifts[PresentType.one] = sd.gift1;
        DataManager.instance.gifts[PresentType.two] = sd.gift2;
        DataManager.instance.gifts[PresentType.four] = sd.gift3;
        DataManager.instance.gifts[PresentType.five] = sd.gift4;

        DataManager.instance.canDestroy = sd.canDestroy;
        DataManager.instance.canBoost = sd.canBoost;
        DataManager.instance.affinityBoost = sd.affinityBoost;
        DataManager.instance.canHack = sd.canHack;
        DataManager.instance.canThrow = sd.canThrow;
        DataManager.instance.piperExamine = sd.piperExamine;

        DataManager.instance.carParts = sd.carparts;
        DataManager.instance.partsneeded = sd.partsneeded;

        DataManager.instance.textspeed = sd.textspeed;
        DataManager.instance.SetName(sd.playername);
        DataManager.instance.am.bgmvol = sd.bgmvolume;
        DataManager.instance.am.sfxvol = sd.sfxvolume;

        //DataManager.instance.lastscene = sd.currentscene;
        SceneManager.LoadScene(sd.currentscene);


    }
    // Update is called once per frame
    void Update()
    {
        
    }


    //The save data class that is 
    private class SaveData
    {
        public int phase;

        //confidant exp levels
        public int piperexp;
        public int springexp;
        public int dexexp;
        public int mustangexp;

        //Gift Counts
        public int gift1;
        public int gift2;
        public int gift3;
        public int gift4;

        // abilities
        public bool canDestroy;
        public bool canBoost;
        public bool affinityBoost;
        public bool canHack;
        public bool canThrow;
        public bool piperExamine;

        //car parts
        public int carparts;
        public int partsneeded;

        //settings
        public float textspeed;
        public string playername;
        public float bgmvolume;
        public float sfxvolume;

        //level
        public string currentscene;
    }
    
}


