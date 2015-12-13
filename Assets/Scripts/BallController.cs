using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	SkinnedMeshRenderer shapes;

	[SerializeField]
	float maximumScale = 1.0f;

	[SerializeField]
	float minimumScale = 0.25f;

	[SerializeField]
	float scaleSpeed = 0.05f;

	[SerializeField]
	float height = 5.0f;

	[SerializeField]
	float hop = 9.0f;


	Rigidbody rBody;
	[SerializeField]
	bool onGround = false;

	AudioSource audioFlop;
	AudioSource audioInflate;
	AudioSource audioDeflate;

	float zPosition;

	// Use this for initialization
	void Start () {
		shapes = GetComponentInChildren<SkinnedMeshRenderer>();
		rBody = GetComponent<Rigidbody>();
		zPosition = transform.position.z;
		AudioSource[] audios = GetComponents<AudioSource>();
		audioFlop = audios[0];
		audioInflate = audios[1];
		audioDeflate = audios[2];
	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = new Ray(transform.position, Vector3.down);
		RaycastHit[] hits = Physics.RaycastAll(ray, height);

		if (hits.Length > 0)
		{
			if (onGround == false)
				audioFlop.Play();
			onGround = true;
		}
		else
		{
			onGround = false;
		}


		if (Input.GetKey(KeyCode.Z))
		{
			float newScale = transform.localScale.x;

			if (newScale == minimumScale)
			{
				audioInflate.Play();

				if (onGround)
				{
					rBody.AddForce(new Vector3(hop, hop, 0f), ForceMode.Impulse); //Jump
					
				}
			}

			newScale += scaleSpeed;
			if (newScale > maximumScale)
				newScale = maximumScale;
			
			transform.localScale = new Vector3(newScale, newScale, newScale);
			Animate(newScale);
			
		}

		if (Input.GetKey(KeyCode.X))
		{
			float newScale = transform.localScale.x;

			if (newScale == maximumScale)
			{
				audioDeflate.Play();
			}

			newScale -= scaleSpeed;
			if (newScale < minimumScale)
				newScale = minimumScale;

			transform.localScale = new Vector3(newScale, newScale, newScale);
			Animate(newScale);
			
		}

		//Keep from drifting off the track
		Vector3 clampedPosition = transform.position;
		clampedPosition.z = zPosition;
		transform.position = clampedPosition;

		//Debug.Log(rBody.velocity.magnitude);
		if(rBody.velocity.magnitude > 30.0f)
		{
			rBody.velocity = rBody.velocity.normalized * 30.0f;
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
