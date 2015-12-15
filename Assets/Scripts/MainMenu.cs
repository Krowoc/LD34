using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
 
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
                Application.Quit();
            else
                OnBackButton();
        }
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("MainScene001");
    }

    public void OnCreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnBackButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
