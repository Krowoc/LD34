using UnityEngine;
using System.Collections;
using System;

public class ChefController : MonoBehaviour {

    [SerializeField]
    GameObject Pause;

    [SerializeField]
    float runningSpeed = 0.2f;

    [SerializeField]
	float catchupSpeed = 0.03f;

	[SerializeField]
	GameObject target;

	[SerializeField]
	float followOffset = 25.0f;
	[SerializeField]
	float followAdvance = 0.005f;
	//AnimationCurve speedOverTime;
	//float startingTime;


	Animator anim;

    //bool initialStart = false;
	[SerializeField]
	bool stopped = true;
	bool paused = false;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		anim.SetTrigger("Chasing");

		//startingTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
	    if (Pause.GetComponent<PauseManager>().pauseEnabled == true)
        {
			paused = true;
        }
        else
        {
			paused = false;
        }


		if (stopped || paused)
			return;
		
		Vector3 newPosition = transform.position;

		followOffset -= followAdvance;
		if (followOffset < 0.0f)
			followOffset = 0.0f;

		//Move forward
		newPosition.x = Mathf.Lerp(transform.position.x, target.transform.position.x - followOffset, catchupSpeed);
		if (newPosition.x < transform.position.x + runningSpeed)
			newPosition.x = transform.position.x + runningSpeed;


		//Keep on the ground
		//Ignore "Prefabs" layer
		int layerMask = 1 << 9;
		layerMask = ~layerMask;

		//Measure from above to detect hills
		float offset = 2.0f;
		Vector3 groundCheck = transform.position;
		groundCheck.y += offset;

		//Cast ray and set position
		RaycastHit hit;
		Physics.Raycast(groundCheck, Vector3.down, out hit, 20.0f, layerMask, QueryTriggerInteraction.Ignore);
		newPosition.y -= hit.distance - offset;


		//If ahead of fish, stop
		if(transform.position.x > target.transform.position.x)
		{
			//newPosition.x = transform.position.x;
			StartCoroutine(ChefStopCoroutine(1.0f));
			//newPosition.x -= runningSpeed;//speedOverTime.Evaluate((Time.time - startingTime) * 0.1f);
			anim.SetBool("Running", false);
		}
		else
		{
			//stopped = false;
			anim.SetBool("Running", true);
		}

		//Limit speed, specifically for steep hills TODO: needs more thorough testing
		Vector3 positionDiff = newPosition - transform.position;
		Vector3.ClampMagnitude(positionDiff, 0.4f);

		//Apply transformations
		//transform.position = newPosition;
		transform.position += positionDiff;


	}

	public void Win()
	{
		anim.SetTrigger("KnifeWave");
	}

	void OnTriggerEnter(Collider other)
	{
		FishController fish = other.GetComponent<FishController>();

		if(fish != null)
		{
			fish.Death();
			Win();

			GetComponent<AudioSource>().Play();
		}

	}

	public void HitBeehive()
	{
		if (stopped)
			return;

		StartCoroutine(HitBeehiveCoroutine());

	}

	public IEnumerator HitBeehiveCoroutine()
	{

		followOffset += 1.0f;

		anim.SetTrigger("KnifeWave");

		yield return StartCoroutine(ChefStopCoroutine(2.0f));

		anim.SetTrigger("Chasing");
	}

	public void StopMoving(float duration)
	{
		StartCoroutine(ChefStopCoroutine(duration));
	}

	public void StartMoving()
	{
		stopped = false;
	}

	IEnumerator ChefStopCoroutine(float duration)
	{
		stopped = true;

		yield return new WaitForSeconds(duration);
			
		stopped = false;

	}
}
