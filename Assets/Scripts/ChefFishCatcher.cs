using UnityEngine;
using System.Collections;

public class ChefFishCatcher : MonoBehaviour {

	[SerializeField]
	ChefController chef;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		BallController fish = other.GetComponent<BallController>();

		if(fish != null)
		{
			fish.Death();
			chef.Win();

			GetComponent<AudioSource>().Play();
		}

	}
}
