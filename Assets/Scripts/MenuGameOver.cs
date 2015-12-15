using UnityEngine;
using System.Collections;

public class MenuGameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnRestart()
	{
		Manager.singleton.EndLevel("MainScene001");
	}

	public void OnMainMenu()
	{
		Manager.singleton.EndLevel("MainMenu");
	}
}
