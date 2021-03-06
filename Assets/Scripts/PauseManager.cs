﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public Canvas pauseMenu;
    public bool pauseEnabled = false;
    public const float PauseSpeed = 0.0000001f;

    void Start()
    {
        pauseEnabled = false;
        Time.timeScale = 1;
        AudioListener.volume = 1;
        //UnityEngine.Cursor.visible = false;
        pauseMenu = pauseMenu.GetComponent<Canvas>();
        pauseMenu.enabled = false;
    }

    void Update()
    {

		if(Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene("MainScene001");
		}

        //check if pause button (escape key) is pressed
        if (Input.GetKeyDown("escape"))
        {

            //check if game is already paused		
            if (pauseEnabled == true)
            {
                //unpause the game
                pauseEnabled = false;
                pauseMenu.enabled = false;
                Time.timeScale = 1;
                AudioListener.volume = 1;
                //UnityEngine.Cursor.visible = false;			
            }

            //else if game isn't paused, then pause it
            else if (pauseEnabled == false)
            {
                pauseEnabled = true;
                pauseMenu.enabled = true;
                AudioListener.volume = 0;
                Time.timeScale = PauseSpeed;
                //UnityEngine.Cursor.visible = true;
            }
        }
    }


    public void OnResumeButton()
    {
        //unpause the game
        pauseEnabled = false;
        pauseMenu.enabled = false;
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    public void OnMainMenuButton()
    {
        OnResumeButton();
		Manager.singleton.EndLevel("MainMenu");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}