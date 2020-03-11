using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickShift : MonoBehaviour
{
    [SerializeField] private List<GameObject> gearpos;
    [SerializeField] private GameObject currentgear;
    // Start is called before the first frame update
    void Start()
    {
        gearpos[0].SetActive(true);
        currentgear = gearpos[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Changes which gear is displayed based on the call of a function
    //generally, each is mapped to a button hover
    public void displaygear(int gearindex)
    {
        currentgear.SetActive(false);
        currentgear = gearpos[gearindex];
        currentgear.SetActive(true);
    }
}
