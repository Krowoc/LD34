using UnityEngine;
using System.Collections;

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

    bool initialStart = false;
	bool isStarted = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.SetTrigger("Chasing");

		//startingTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
	    if (Pause.GetComponent<PauseManager>().pauseEnabled == true)
        {
            isStarted = false;
        }
        else if (initialStart == true)
        {
            isStarted = true;
        }


		if (!isStarted)
			return;

        initialStart = true;

		Vector3 newPosition = transform.position;

		followOffset -= followAdvance;
		if (followOffset < 0.0f)
			followOffset = 0.0f;
		//Move forward
		//newPosition.x += runningSpeed;
		//newPosition.x += speedOverTime.Evaluate((Time.time - startingTime) * 0.1f);
		/*if (transform.position.x > target.transform.position.x - followOffset - 4.0f)
		{
			newPosition.x += runningSpeed;
			//newPosition.x = Mathf.Lerp(transform.position.x, target.transform.position.x - followOffset, catchupSpeed);
		}
		else
		{
			newPosition.x = Mathf.Lerp(transform.position.x, target.transform.position.x - followOffset, catchupSpeed);
		}*/

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


		//Get distance to fish
		float distance = target.transform.position.x - newPosition.x;

		//If way behind, catch up
		/*if (distance > catchupDistance)
		{
			newPosition.x += 10.0f;//= target.transform.position.x - catchupDistance;
		}*/

		//If ahead of fish, stop
		if(distance < 0)
		{
			newPosition.x -= runningSpeed;//speedOverTime.Evaluate((Time.time - startingTime) * 0.1f);
			anim.SetBool("Running", false);
		}
		else
		{
			anim.SetBool("Running", true);
		}

		//Apply transformations
		transform.position = newPosition;


	}

	public void Win()
	{
		anim.SetTrigger("KnifeWave");
	}

	public void StartRunning()
	{
		isStarted = true;
	}

	void OnTriggerEnter(Collider other)
	{
		BallController fish = other.GetComponent<BallController>();

		if(fish != null)
		{
			fish.Death();
			Win();

			GetComponent<AudioSource>().Play();
		}

	}
}
