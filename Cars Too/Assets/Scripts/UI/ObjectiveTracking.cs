using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ObjectiveTracking : MonoBehaviour
{
    private TextMeshProUGUI objtext = null;
    [SerializeField] private bool AutoProceed = false; //whether to auto go to the next level when the parts are collected
    [SerializeField] private string destscene;

    [SerializeField] private GameObject objcomplete = null;
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
        objtext.text = "Gather Parts: " + DataManager.instance.carParts + "/"+ DataManager.instance.partsneeded;
        if (DataManager.instance.carParts >= DataManager.instance.partsneeded)
        {
            objtext.fontStyle = FontStyles.Strikethrough| FontStyles.Bold;
            objcomplete.SetActive(true);
            if (AutoProceed)
            {
                SceneManager.LoadScene(destscene);
            }
        }
        
    }

    public void SetPartsNeeded(int n)
    {
        DataManager.instance.partsneeded = n;
        UpdateQuest();
        objtext.fontStyle =FontStyles.Bold;
        objcomplete.SetActive(false);
    }
}
