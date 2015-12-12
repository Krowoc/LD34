using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	[SerializeField]
	GameObject followObject;

	[SerializeField]
	float followSpeed;

	float zPosition;

	// Use this for initialization
	void Start () {

		zPosition = transform.position.z;

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newPosition = Vector3.Lerp(transform.position, followObject.transform.position, Time.deltaTime * followSpeed);
		newPosition.z = Mathf.Lerp(transform.position.z, followObject.transform.localScale.x * -10.0f, Time.deltaTime * followSpeed);

		transform.position = newPosition;
		
	}
}
