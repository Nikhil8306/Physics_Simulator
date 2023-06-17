using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_UI : MonoBehaviour
{

    public void OnSimulator()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnThreeBody()
    {
        Illustrations.threeBody = true;
        SceneManager.LoadScene(1);
    }

    public void OnSolar()
    {
        Illustrations.solar = true;
        SceneManager.LoadScene(1);
    }

    public void OnSolar1()
    {
        Illustrations.solar1 = true;
        SceneManager.LoadScene(1);
    }

}
