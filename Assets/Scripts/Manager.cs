using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : SingletonMonoBehaviour<Manager>
{

	int catScore = 0;
	float distancceScore = 0.0f;
	float airTimeScore = 0.0f;

	Text scoreText;
	Text airText;

	// Use this for initialization
	void Start () {

		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

	}

	public void updateCatScore()
	{
		catScore++;


	}

	public void updateDistanceScore(float distance)
	{
		if (distance > distancceScore)
			distancceScore = distance;

		int d = (int)distancceScore;
		scoreText.text = d.ToString();
	}

	public void updateAirTimeScore(float airTime)
	{
		if (airTime > airTimeScore)
			airTimeScore = airTime;


	}

}
