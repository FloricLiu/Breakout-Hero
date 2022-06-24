using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Manager3 : MonoBehaviour
{
    public GameObject[] enemies;
    public bool gamePause = false;
    public GameObject menu;
    public GameObject win;
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Checks if enemies are available with tag "Enemy". Note that you should set this to your enemies in the inspector.
        if (enemies.Length == 0)
        {
            Time.timeScale = 0;
            gamePause = true;
            win.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePause == false)
            {
                Time.timeScale = 0;
                gamePause = true;
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
                Time.timeScale = 1;
                gamePause = false;
            }
        }
    }
    public void MenuBotton()
    {
        SceneManager.LoadScene("Global");
        Time.timeScale = 1;
    }
    public void ResumeBotton()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        gamePause = false;
    }
    public void ExitBotton()
    {
        Application.Quit();
    }
}
