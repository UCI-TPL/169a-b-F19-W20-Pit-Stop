using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PresentType {one, two, three, four, five};

public class PresentBox : MonoBehaviour
{

    [SerializeField] PresentType myPresentType;
    [SerializeField] int id=0;

    private int numberOfPresentTypes = 5;

    // Start is called before the first frame update
    void Start()
    {

        if (DataManager.instance.ContainsIdGift(id))
        {
            Destroy(this.gameObject);
            return;
        }

        myPresentType = (PresentType) Random.Range(0, numberOfPresentTypes);
        Debug.Log(myPresentType);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PresentType GetPresentType()
    {
        return myPresentType;
    }

    public int GetID()
    {
        return id;
    }
}
