using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowScores : MonoBehaviour {

	[SerializeField]
	Text hiScoreText;

	[SerializeField]
	Text scoreText;

	[SerializeField]
	Text jumpScoreText;

	[SerializeField]
	Text catScoreText;

	// Use this for initialization
	void Start () {

		PlayerPrefs.SetFloat("HiScore", Manager.singleton.GetHighScore());

		int hiScore = (int)Manager.singleton.GetHighScore();
		hiScoreText.text = hiScore.ToString();

		int score = (int)Manager.singleton.GetDistanceScore();
		scoreText.text = score.ToString();

		int jumpScore = (int)Manager.singleton.GetAirScore();
		string seconds = " seconds";
		if (jumpScore == 1)
			seconds = " second";
		jumpScoreText.text = jumpScore.ToString() + seconds;

		int catScore = Manager.singleton.GetCatScore();
		if (catScore == 0)
			catScoreText.text = "Pacifist!";
		else
			catScoreText.text = catScore.ToString();
		
		Manager.singleton.ResetScores();

		//TODO: put hiscore saving here

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
