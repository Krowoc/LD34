using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	//Rigidbody2D rBody;

	[SerializeField]
	float maximumScale = 1.0f;

	[SerializeField]
	float minimumScale = 0.25f;

	[SerializeField]
	float scaleSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		//rBody = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Z))
		{
			float newScale = transform.localScale.x;
			newScale += scaleSpeed;
			if (newScale > maximumScale)
				newScale = maximumScale;

			transform.localScale = new Vector3(newScale, newScale, newScale);

		}

		if (Input.GetKey(KeyCode.X))
		{
			float newScale = transform.localScale.x;
			newScale -= scaleSpeed;
			if (newScale < minimumScale)
				newScale = minimumScale;

			transform.localScale = new Vector3(newScale, newScale, newScale);

		}
	}
}
