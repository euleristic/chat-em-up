using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
            SceneScript.LoadGame();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
