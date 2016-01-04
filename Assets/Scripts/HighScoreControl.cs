using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreControl : MonoBehaviour
{
	private string secretKey = "101kr0w0c101001"; //double-check this to confirm the same as server side...
	public string addScoreURL = "http://krowoc.net/Games/SushiRoll/addscore.php?"; //add ? for url additions
	public string highscoreURL = "http://krowoc.net/Games/SushiRoll/highscores.php";
	public Text LeaderboardText;
	public Canvas ScoreMenu;
	public Canvas HallofFame;
	public Button SubmitButton;

	public Text ScoreDisplay;
	public int ScoreDisplayInt;
	public Text NameDisplayText;
	public string NameDisplay;
	public float WaitWWW;

	public void OnShowScoreMenu()
	{
		ScoreMenu.enabled = true;
	}

	public void OnHideScoreMenu()
	{
		ScoreMenu = ScoreMenu.GetComponent<Canvas>();
		ScoreMenu.enabled = false;
	}

	public void OnHideHallofFame()
	{
		HallofFame = HallofFame.GetComponent<Canvas>();
		HallofFame.enabled = false;
	}


	public static int ToInt32(string value)
	{
		if (value == null)    
			return 0;
		return System.Int32.Parse(value, System.Globalization.CultureInfo.CurrentCulture);
	}

	public void OnSubmitButton()
	{
		ScoreDisplay = GameObject.Find ("ScoreText2").GetComponent<Text>();
		ScoreDisplayInt = ToInt32 (ScoreDisplay.text);
		NameDisplayText = GameObject.Find ("InputText").GetComponent<Text>();
		NameDisplay = NameDisplayText.text;
		Debug.Log (NameDisplay.ToString());
		StartCoroutine(GameObject.Find ("Score").GetComponent<HighScoreControl>().PostScores(NameDisplay, ScoreDisplayInt));

		ScoreMenu = ScoreMenu.GetComponent<Canvas>();
		ScoreMenu.enabled = false;
		HallofFame = HallofFame.GetComponent<Canvas> ();
		HallofFame.enabled = true;
		SubmitButton = SubmitButton.GetComponent<Button> ();
		SubmitButton.gameObject.SetActive (false);

		StartCoroutine (Wait (WaitWWW));
		StartCoroutine(GetScores());
	}


	public  string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
		
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
		
		return hashString.PadLeft(32, '0');
	}
	
	void Start()
	{
		HallofFame.enabled = false;
		ScoreMenu.enabled = false;
	}

	// using StarCoroutine
	public IEnumerator PostScores(string name, int score)
	{
		//This connects to a server side php script that will add the name and score to a MySQL DB.
		// Supply it with a string representing the players name and the players score.
		string hash = Md5Sum(name + score + secretKey);
		
		string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
		
		// Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW(post_url);

        ///Debug.Log(post_url);
		yield return hs_post; // Wait until the download is done

		if (hs_post.error != null)
		{
			print("There was an error posting the high score: " + hs_post.error);
		}
	}
	
	// Get the scores from MySQL
	// using StartCoroutine
	IEnumerator GetScores()
	{
		LeaderboardText.text = "Loading Scores";
		WWW hs_get = new WWW(highscoreURL);
		yield return hs_get;
		
		if (hs_get.error != null)
		{
			print("There was an error getting the high score: " + hs_get.error);
		}
		else
		{
			LeaderboardText.text = hs_get.text;
		}
	}

	IEnumerator Wait(float duration)
	{
		Debug.Log("Start Wait() function. The time is: "+Time.time);
		yield return new WaitForSeconds (duration);
		Debug.Log("End Wait() function and the time is: "+Time.time);
	}
	
}