using UnityEngine;
using System.Collections;

public class CatTrigger : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	

	}

	void OnTriggerEnter(Collider collider)
	{
		Debug.Log("Trigger Enter");

		BallController fish = collider.gameObject.GetComponent<BallController>();
		if (fish == null)
			return;

		anim.SetTrigger("Pounce");

		if (fish.isCaught())
			Debug.Log("Caught");
			
	}
}
