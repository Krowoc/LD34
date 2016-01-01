using UnityEngine;
using System.Collections;

public class CatTrigger : MonoBehaviour {

	[SerializeField]
	CatController cat;

	// Use this for initialization
	void Start () {
		cat = GetComponentInChildren<CatController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider)
	{
		FishController fish = collider.gameObject.GetComponent<FishController>();
		if (fish == null)
			return;

		if (fish.isCaught())
		{
			fish.Death();
			GetComponent<AudioSource>().Play();
			//Debug.Log("Caught");
		}
		else
		{
			cat.Death();
			GameObject.Destroy(transform.parent.gameObject);
		}
			
	}

}
