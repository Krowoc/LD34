using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	[SerializeField]
	float scrollSpeed = 0.0005f;

	[SerializeField]
	float scrollDistance = 16.0f;

	[SerializeField]
	float startingPosition;
	float offset = 0.0f;

	// Use this for initialization
	void Start () {
		startingPosition = transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {

		offset += scrollSpeed;
		if (offset >= scrollDistance)
		{
			offset = scrollDistance;
		}

		Vector3 newPosition = transform.localPosition;

		newPosition.y = startingPosition + offset;

		transform.localPosition = newPosition;
	}
}
