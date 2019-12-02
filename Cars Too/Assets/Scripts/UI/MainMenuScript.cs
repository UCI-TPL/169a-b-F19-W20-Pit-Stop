using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private string startingscene= "IntroLevel";
    public void StartGame()
    {
        DataManager.instance.HardReset();
        DataManager.instance.canBoost = true;
        DataManager.instance.canDestroy = true;
        DataManager.instance.canHack = true;
        DataManager.instance.canThrow = true;
        SceneManager.LoadScene(startingscene);
    }

    public void ContinueGame()
    {
        if(!DataManager.instance.lastscene.Equals(""))
            SceneManager.LoadScene(DataManager.instance.lastscene);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
