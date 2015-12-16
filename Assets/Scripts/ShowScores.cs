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
		//Debug.Log(Manager.singleton.GetDistanceScore());
		//scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		PlayerPrefs.SetFloat("HiScore", Manager.singleton.distanceHighScore);

		int hiScore = (int)Manager.singleton.distanceHighScore;
		hiScoreText.text = hiScore.ToString();

		int score = (int)Manager.singleton.distanceScore;
		scoreText.text = score.ToString();
		Manager.singleton.distanceScore = 0.0f;

		int jumpScore = (int)Manager.singleton.airTimeScore;
		string seconds = " seconds";
		if (jumpScore == 1)
			seconds = " second";
		jumpScoreText.text = jumpScore.ToString() + seconds;
		Manager.singleton.airTimeScore = 0.0f;

		int catScore = Manager.singleton.catScore;
		if (catScore == 0)
			catScoreText.text = "Pacifist!";
		else
			catScoreText.text = catScore.ToString();
		Manager.singleton.catScore = 0;

		//TODO: put hiscore saving here

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
