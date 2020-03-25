using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PLayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void LoadCotrolls()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);

    }

    public void LoadMenu2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 5);

    }
    public void LoadChattApp()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);

    }
    public void QuitGame() 
    {
        Debug.Log ("Quit");
        Application.Quit();
    }
}
