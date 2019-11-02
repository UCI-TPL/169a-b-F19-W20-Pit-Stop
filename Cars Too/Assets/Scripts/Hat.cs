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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
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

        GameObject temphat = Instantiate(hatprefab,this.transform);
        temphat.transform.parent = null;
        startingpointz = temphat.transform.position.z;
        startingpointx = temphat.transform.position.x;
        visualhat.SetActive(false);

        while (Mathf.Sqrt(Mathf.Pow(temphat.transform.position.z-startingpointz,2)+ Mathf.Pow(temphat.transform.position.x - startingpointx, 2)) < throwdist)
        {
            temphat.transform.Translate(Vector3.forward * speed*Time.deltaTime);
            yield return null;
        }
        Destroy(temphat.gameObject);
        visualhat.SetActive(true);
    }
}
