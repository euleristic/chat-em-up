using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.G) || Input.GetKey(KeyCode.R)) LoadGame();
        if (Input.GetKey(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name != "Menu") LoadMenu();
            else Application.Quit();
        }
    }

    static public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    static public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    static public void WinState()
    {
        SceneManager.LoadScene("WonScene");
    }

    static public void LoseState()
    {
        SceneManager.LoadScene("LostScene");
    }

    static public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}