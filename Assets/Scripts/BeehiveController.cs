using UnityEngine;
using System.Collections;

public class BeehiveController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		FishController fish = other.GetComponent<FishController>();

		if (fish != null)
		{
			
		}

		ChefController chef = other.GetComponent<ChefController>();

		if (chef != null)
		{
			
		}
	}
}
