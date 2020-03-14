using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfDial : MonoBehaviour
{
    [SerializeField] private List<GameObject> dialpos;
    [SerializeField] private GameObject currentdial;
    // Start is called before the first frame update
    void Start()
    {
        dialpos[0].SetActive(true);
        currentdial = dialpos[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Changes which dial is displayed based on the call of a function
    //generally, each is mapped to a button hover
    public void updatedial(int dialindex)
    {
        currentdial.SetActive(false);
        currentdial = dialpos[dialindex];
        currentdial.SetActive(true);
    }
}
