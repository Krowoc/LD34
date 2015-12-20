using UnityEngine;
using System.Collections;

public class CatAnimationTrigger : MonoBehaviour {

	CatController cat;

	// Use this for initialization
	void Start()
	{
		cat = GetComponentInChildren<CatController>();
	}

	// Update is called once per frame
	void Update()
	{


	}

	void OnTriggerEnter(Collider collider)
	{
		FishController fish = collider.gameObject.GetComponent<FishController>();
		if (fish == null)
			return;

		cat.Pounce();
		
	}
}
