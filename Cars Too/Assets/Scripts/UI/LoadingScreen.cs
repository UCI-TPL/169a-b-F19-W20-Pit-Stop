using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject tire1;
    [SerializeField] private GameObject tire2;
    [SerializeField] private float tirespeed = 90.0f;
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
        tire1.transform.parent.gameObject.SetActive(true);
        StartCoroutine(LoadAsyncScene(destscene));
    }

    private IEnumerator LoadAsyncScene(string destscene)
    {

        AsyncOperation loadlvl = SceneManager.LoadSceneAsync(destscene);

        while (loadlvl.progress < 1)
        {

            tire1.transform.Rotate(0, 0, tirespeed * (loadlvl.progress+.25f));
            tire2.transform.Rotate(0, 0, tirespeed * (loadlvl.progress + .25f));
            yield return new WaitForEndOfFrame();
        }
    }
}
