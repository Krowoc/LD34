using UnityEngine;
using System.Collections;

public class MenuMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//bool f = Manager.singleton.fading;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPlay()
	{
		Manager.singleton.EndLevel("MainScene001");
	}

	public void OnCredits()
	{
		Manager.singleton.EndLevel("Credits");
	}

	public void OnQuit()
	{
		Application.Quit();
	}
}
