using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject NewGame;
    [SerializeField] private GameObject Quit;
    bool currentSelection;

    void Start()
    {
        currentSelection = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentSelection = !currentSelection;
            if (currentSelection)
            {
                Quit.GetComponent<GUIText>().color = Color.green;
                NewGame.GetComponent<GUIText>().color = Color.white;
            }
            else
            {
                Quit.GetComponent<GUIText>().color = Color.white;
                NewGame.GetComponent<GUIText>().color = Color.green;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentSelection)
                Application.Quit();
            else
                SceneScript.LoadGame();
        }
    }
}
