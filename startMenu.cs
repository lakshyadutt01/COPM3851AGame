using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{

    public Canvas Menu;
    public Button play;
    public int health = 3;
    public int jump = 0;
    public int dash = 0;
    public int wall = 0;

    void Start()
    {
        play = play.GetComponent<Button>();
        Menu = Menu.GetComponent<Canvas>();
        Menu.enabled = true;      
    }


    public void StartGame()
    {
        PlayerPrefs.SetInt("CurrentHealth", health);
        PlayerPrefs.SetInt("ExtraJumpsValue", jump);
        PlayerPrefs.SetInt("ExtraDashValue", dash);
        PlayerPrefs.SetInt("ExtraWallJumpValue", wall);
        SceneManager.LoadScene(1);
    }
}
