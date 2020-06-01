using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public Canvas gameoverScreen;
    public Button restart;



    void Start()
    {
        gameoverScreen = gameoverScreen.GetComponent<Canvas>();
        restart = restart.GetComponent<Button>();
        gameoverScreen.enabled = false;

    }

    void Update()
    {
        PlayerController controller = GetComponent<PlayerController>();

        if (controller.currentHealth <= 0)
        {
            gameoverScreen.enabled = true;
        }
    }

    public void Restart()
    {
        Debug.Log("wow howq buggy lmao");
        SceneManager.LoadScene(0);
    }
}
