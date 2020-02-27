using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private CarMovement carmov = null;
    [SerializeField] private GameObject pm;
    [SerializeField] private GameObject basepm;
    // Start is called before the first frame update
    void Start()
    {
        carmov = GameObject.FindObjectOfType<CarMovement>();
        pm.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pm.activeSelf)
            {
                ClosePauseMenu();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    public void OpenPauseMenu()
    {
        carmov.Pause();
        pm.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        if (basepm.activeSelf)
        {
            carmov.Unpause();
            pm.SetActive(false);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
