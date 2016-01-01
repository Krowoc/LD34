using UnityEngine;
using System.Collections;

public class FishController : MonoBehaviour {

	SkinnedMeshRenderer shapes;

	public float scale = 0.0f;

	[SerializeField]
	float minimumScale = 0.25f;

	[SerializeField]
	float maximumScale = 1.0f;

	[SerializeField]
	float superScale = 1.5f;

	[SerializeField]
	float scaleSpeed = 0.05f;

	[SerializeField]
	float height = 5.0f;

	[SerializeField]
	float hopForce = 9.0f;

	[SerializeField]
	float maxVelocity = 30.0f;


	[SerializeField]
	bool onGround = false;

	Rigidbody rBody;

	AudioSource audioFlop;
	AudioSource audioInflate;
	AudioSource audioDeflate;

	float airTime;
	float airStartTime;

	float zPosition;

	bool isDead = false;
	bool isStarted = false;

	float scaleLimit = 1.0f;

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

		if (isDead || !isStarted)
			return;

		Manager.singleton.updateDistanceScore(transform.position.x);

		Ray ray = new Ray(transform.position, Vector3.down);
		RaycastHit[] hits = Physics.RaycastAll(ray, height);

		//If the character is in contact with the ground
		if (hits.Length > 0)
		{
			//If landing
			if (onGround == false)
			{
				airTime = Time.time - airStartTime;

				Manager.singleton.updateAirTimeScore(airTime);

				audioFlop.Play();
			}

			onGround = true;
		}
		else
		{
			//If taking off
			if (onGround == true)
				airStartTime = Time.time;

			onGround = false;
		}

		//Hop
		if(Input.GetButtonDown("Inflate"))
		{
			if (onGround)
			{
				float f = hopForce * ((- scale) + 1.0f);
				Jump(f);
			}

		}

		
		//SuperInflate
		if (Input.GetButton("Inflate") && Input.GetButton("Deflate"))
		{
			if(scale == 1.0f)
				//Jump(10.0f);

			scaleLimit = 2.0f;

			scale += scaleSpeed;
		}
		else 
		{
			if (scaleLimit > 1.0f)
				scaleLimit -= scaleSpeed;
			if (scaleLimit < 1.0f)
				scaleLimit = 1.0f;
			
		}

		//Inflate
		if (Input.GetButton("Inflate") && !Input.GetButton("Deflate"))
		{
			if (scale == 0.0f)
			{
				audioInflate.Play();
			}

			scale += scaleSpeed;

		}

		//Deflate
		if (Input.GetButton("Deflate") && !Input.GetButton("Inflate"))
		{
			if (scale == 1.0f)
			{
				audioDeflate.Play();
			}

			scale -= scaleSpeed;

		}

		//Limit scale
		if (scale > scaleLimit)
			scale = scaleLimit;
		if (scale < 0.0f)
			scale = 0.0f;

		Scale();

		//Keep from drifting off the track
		Vector3 clampedPosition = transform.position;
		clampedPosition.z = zPosition;
		transform.position = clampedPosition;

		//Limit speed
		if(rBody.velocity.magnitude > maxVelocity)
		{
			rBody.velocity = rBody.velocity.normalized * maxVelocity;
		}



	}

	public void Jump(float force)
	{
		rBody.AddForce(new Vector3(hopForce, hopForce, 0f), ForceMode.Impulse);
	}

	void Scale()
	{
		//Normal Scale
		if (scale <= 1.0f)
		{
			//Size
			float newScale = Mathf.Lerp(minimumScale, maximumScale, scale);
			transform.localScale = new Vector3(newScale, newScale, newScale);

			//Animation
			float s = Mathf.Lerp(100.0f, 0.0f, scale);
			shapes.SetBlendShapeWeight(0, s);
			shapes.SetBlendShapeWeight(1, 0f);
		}
		//Super Scale
		else 
		{
			//Size
			float newScale = Mathf.Lerp(maximumScale, superScale, scale - 1.0f);
			transform.localScale = new Vector3(newScale, newScale, newScale);

			//Animation
			float s = Mathf.Lerp(0.0f, 100.0f, scale - 1.0f);
			shapes.SetBlendShapeWeight(1, s);
			shapes.SetBlendShapeWeight(0, 0f);
		}

	}

	public bool isCaught()
	{
		if(scale > 0.5f)
			return false;
		else
		{
			//Death();
			return true;
		}
		
	}

	public void Death()
	{
		if (isDead)
			return;
		rBody.velocity = Vector3.zero;
		rBody.freezeRotation = true;
		isDead = true;

		Manager.singleton.EndLevel("GameOverScene");
	}

	public void StartRolling()
	{
		isStarted = true;
	}
}

