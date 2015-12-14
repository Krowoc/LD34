using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : SingletonMonoBehaviour<Manager>
{

	Text scoreText;
	Text airtext;

	// Use this for initialization
	void Start () {

		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
