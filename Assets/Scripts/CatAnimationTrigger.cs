using UnityEngine;
using System.Collections;

public class CatAnimationTrigger : MonoBehaviour {

	Animator anim;
	CatController cat;

	// Use this for initialization
	void Start()
	{
		anim = GetComponentInChildren<Animator>();
		cat = GetComponentInChildren<CatController>();
	}

	// Update is called once per frame
	void Update()
	{


	}

	void OnTriggerEnter(Collider collider)
	{
		BallController fish = collider.gameObject.GetComponent<BallController>();
		if (fish == null)
			return;

		cat.Pounce();
		
	}
}
