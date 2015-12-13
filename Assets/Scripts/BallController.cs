﻿using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	SkinnedMeshRenderer shapes;

	[SerializeField]
	float maximumScale = 1.0f;

	[SerializeField]
	float minimumScale = 0.25f;

	[SerializeField]
	float scaleSpeed = 0.1f;

	[SerializeField]
	float height = 1.0f;


	Rigidbody rBody;
	[SerializeField]
	bool onGround = false;

	// Use this for initialization
	void Start () {
		shapes = GetComponentInChildren<SkinnedMeshRenderer>();
		rBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = new Ray(transform.position, Vector3.down);
		RaycastHit[] hits = Physics.RaycastAll(ray, height);

		if (hits.Length > 0)
			onGround = true;
		else
			onGround = false;


		if(Input.GetKey(KeyCode.Z))
		{
			float newScale = transform.localScale.x;

			if (onGround && newScale == minimumScale)
				rBody.AddForce(new Vector3(9f, 9f, 0f), ForceMode.Impulse); //Jump

			newScale += scaleSpeed;
			if (newScale > maximumScale)
				newScale = maximumScale;
			
			transform.localScale = new Vector3(newScale, newScale, newScale);
			Animate(newScale);
			
		}

		if (Input.GetKey(KeyCode.X))
		{
			float newScale = transform.localScale.x;
			newScale -= scaleSpeed;
			if (newScale < minimumScale)
				newScale = minimumScale;

			transform.localScale = new Vector3(newScale, newScale, newScale);
			Animate(newScale);
		}
	}

	void Animate(float scale)
	{
		float s = Mathf.InverseLerp(maximumScale, minimumScale, scale);

		/*s = (s * 200.0f) - 100.0f;

		if (s > 0)
		{
			shapes.SetBlendShapeWeight(1, s);
			shapes.SetBlendShapeWeight(0, 0f);
		}
			
		else
		{
			shapes.SetBlendShapeWeight(0, -s);
			shapes.SetBlendShapeWeight(1, 0f);
		}*/

		s = s * 100.0f;
		shapes.SetBlendShapeWeight(0, s);
		

	}
}
