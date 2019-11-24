using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveTracking : MonoBehaviour
{
    private TextMeshProUGUI objtext = null;
    public int partsneeded = 4;
    // Start is called before the first frame update
    void Start()
    {
        objtext = GetComponent<TextMeshProUGUI>();
        DataManager.instance.carPartAcquired.AddListener(UpdateQuest);
        UpdateQuest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateQuest()
    {
        objtext.text = "Gather Parts: " + DataManager.instance.carParts + "/"+partsneeded;
    }

    public void SetPartsNeeded(int n)
    {
        partsneeded = n;
        UpdateQuest();
    }
}
