﻿using UnityEngine;
using System.Collections;

public class MenuCredits : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnReturn()
	{
		Manager.singleton.EndLevel("MainMenu");
	}
}
