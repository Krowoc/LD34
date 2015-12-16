using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : SingletonMonoBehaviour<Manager>
{
	//public bool fading;

	SceneFade fadeObject;

	int catScore = 0;
	float distanceScore = 0.0f;
	float distanceHighScore;
	float airTimeScore = 0.0f;

	Text scoreText;
	Text airText;

	// Use this for initialization
	void Start () {




	}

	public void updateCatScore()
	{
		catScore++;


	}

	public void updateDistanceScore(float distance)
	{
		if(scoreText == null)
		{
			GameObject go = GameObject.Find("ScoreText");
			if (go != null)
				scoreText = go.GetComponent<Text>();
			else
				return;
		}

		//if (distance > distanceScore)
		if(distance > 0.0f)
			distanceScore = distance;

		if (distanceScore > distanceHighScore)
			distanceHighScore = distanceScore;

		int d = (int)distanceScore;
		scoreText.text = d.ToString();
	}

	public void updateAirTimeScore(float airTime)
	{
		if (airTime > airTimeScore)
			airTimeScore = airTime;


	}

	public void EndLevel(string nextLevel)
	{
		if (fadeObject == null)
		{
			GameObject go = GameObject.Find("FadeObject");
			if (go != null)
				fadeObject = go.GetComponent<SceneFade>();
			else
				return;
		}

		
		StartCoroutine(EndLevelCoroutine(nextLevel));
	}

	IEnumerator EndLevelCoroutine(string nextLevel)
	{
		
		fadeObject.FadeOut(Color.white, 2.0f);


		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene(nextLevel);
	}

	public float GetDistanceScore()
	{
		return distanceScore;
	}
}
