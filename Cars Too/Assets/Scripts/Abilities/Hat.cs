using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
    [SerializeField] float throwdist = 5.0f;
    private float startingpointz =0.0f;
    private float startingpointx = 0.0f;
    [SerializeField] float speed = 5.0f;
    [SerializeField] GameObject hatprefab;
    [SerializeField] GameObject visualhat;
    private bool throwing = false;
    public bool hitwall = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!DataManager.instance.canThrow) {
            visualhat.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManager.instance.canThrow&&!throwing)
        {
            visualhat.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Q)&&DataManager.instance.canThrow)
        {
            TryThrowHat();
        }
    }

    private void TryThrowHat()
    {
        if (visualhat.activeSelf)
        {
            StartCoroutine(ThrowHat());
        }
    }
    private IEnumerator ThrowHat()
    {
        throwing = true;
        GameObject temphat = Instantiate(hatprefab,this.transform);
        temphat.transform.parent = null;
        startingpointz = temphat.transform.position.z;
        startingpointx = temphat.transform.position.x;
        visualhat.SetActive(false);

        while (Mathf.Sqrt(Mathf.Pow(temphat.transform.position.z-startingpointz,2)+ Mathf.Pow(temphat.transform.position.x - startingpointx, 2)) < throwdist&&!hitwall)
        {
            temphat.transform.Translate(Vector3.forward * speed*Time.deltaTime);
            yield return null;
        }
        Destroy(temphat.gameObject);
        visualhat.SetActive(true);
        throwing = false;
        hitwall = false;
    }
}
