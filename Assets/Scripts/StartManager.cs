using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour {

	[SerializeField]
	BallController fish;

	[SerializeField]
	ChefController chef;

	[SerializeField]
	float delay = 2.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine(StartLevel());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator StartLevel()
	{
		yield return new WaitForSeconds(delay);

		Debug.Log("Start");
		fish.StartRolling();
		//fish.Jump(8000.0f);
		Rigidbody frb = fish.gameObject.GetComponent<Rigidbody>();
		frb.AddForce(10f, 10f, 0f, ForceMode.VelocityChange);
		chef.StartRunning();
	}
}
