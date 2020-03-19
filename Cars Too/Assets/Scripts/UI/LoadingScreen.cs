using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject tire1;
    [SerializeField] private GameObject tire2;
    [SerializeField] private float tirespeed = 90.0f;
    private bool loading = false;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadAsyncScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLoad(string destscene)
    {
        if (!loading)
        {
            loading = true;
            tire1.transform.parent.gameObject.SetActive(true);
            StartCoroutine(LoadAsyncScene(destscene));
        }
    }

    private IEnumerator LoadAsyncScene(string destscene)
    {

        AsyncOperation loadlvl = SceneManager.LoadSceneAsync(destscene);

        while (loadlvl.progress < 1)
        {
            loading = true;
            Debug.Log(loadlvl.progress);
            tire1.transform.Rotate(0, 0, tirespeed * (loadlvl.progress+25f*Time.deltaTime));
            tire2.transform.Rotate(0, 0, tirespeed * (loadlvl.progress + 25f*Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
    }
}
