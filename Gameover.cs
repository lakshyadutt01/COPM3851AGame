using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public static bool GameIsOver = false;

    public GameObject GameoverMenuUI;



    void Update()
    {

    }

    public void DeathMenu()
    {
        GameoverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsOver = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
