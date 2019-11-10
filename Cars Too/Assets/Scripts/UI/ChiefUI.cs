using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiefUI : MonoBehaviour
{
    // Start is called before the first frame update
    //static Dictionary<int, bool> conditions = new Dictionary<int, bool>();
    public int numcarpartsneeded = 3;
    [SerializeField] GameObject uipopup = null;
    void Start()
    {
        uipopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManager.instance.carParts >= numcarpartsneeded)
        {
            uipopup.SetActive(true);
        }
    }

    
}
