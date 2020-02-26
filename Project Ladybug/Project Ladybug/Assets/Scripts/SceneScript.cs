﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.G) || Input.GetKey(KeyCode.R)) LoadGame();
        if (Input.GetKey(KeyCode.Escape)) LoadMenu();
        if (Input.GetKey(KeyCode.W)) WinState();
        if (Input.GetKey(KeyCode.L)) LoseState();
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
}
