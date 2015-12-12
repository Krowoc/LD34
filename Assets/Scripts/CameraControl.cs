﻿using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	[SerializeField]
	GameObject followObject;
	Rigidbody2D followRBody;

	[SerializeField]
	float followSpeed;

	[SerializeField]
	float leadDistance = 3.0f;

	float zPosition;

	// Use this for initialization
	void Start () {

		zPosition = transform.position.z;
		followRBody = followObject.GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 followPosition = followObject.transform.position;
		followPosition.x += followRBody.velocity.x * leadDistance;

		Vector3 newPosition = Vector3.Lerp(transform.position, followPosition, Time.deltaTime * followSpeed);
		newPosition.z = zPosition;//Mathf.Lerp(transform.position.z, followObject.transform.localScale.x * -10.0f, Time.deltaTime * followSpeed);
		
		transform.position = newPosition;
		
	}
}
