using UnityEngine;
using System.Collections;

public class ShadowController : MonoBehaviour {

	[SerializeField]
	GameObject fish;

	[SerializeField]
	float growAmount = 3.0f;

	FishController fishController;
	Projector shadow;
	float startingSize;

	// Use this for initialization
	void Start () {
		fishController = fish.GetComponent<FishController>();
		shadow = GetComponent<Projector>();
		startingSize = shadow.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
	
		#if UNITY_WEBGL
		transform.position = fish.transform.position;
		shadow.orthographicSize = startingSize + (fishController.scale * growAmount);
		#endif
	}
}
