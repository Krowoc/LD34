using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowScores : MonoBehaviour {

	Text scoreText;

	// Use this for initialization
	void Start () {
		Debug.Log(Manager.singleton.GetDistanceScore());
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		int score = (int)Manager.singleton.GetDistanceScore();
		scoreText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
