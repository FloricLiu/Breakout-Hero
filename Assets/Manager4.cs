using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Manager4 : MonoBehaviour
{
    public bool gamePause = false;
    public GameObject menu;

    public void Botton1()
    {
        SceneManager.LoadScene("A");
        Time.timeScale = 1;
    }
    public void Botton2()
    {
        SceneManager.LoadScene("B");
        Time.timeScale = 1;
    }
    public void Botton3()
    {
        SceneManager.LoadScene("C");
        Time.timeScale = 1;
    }
    public void ExitBotton()
    {
        Application.Quit();
    }
}
