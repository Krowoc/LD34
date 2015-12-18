using UnityEngine;
using System.Collections;

public class ChefController : MonoBehaviour {

    [SerializeField]
    GameObject Pause;

    //[SerializeField]
    //float runningSpeed = 0.2f;

    [SerializeField]
	float catchupDistance = 100.0f;

	[SerializeField]
	GameObject target;

	[SerializeField]
	AnimationCurve speedOverTime;
	float startingTime;

	float followOffest = 50.0f;

	Animator anim;

    bool initialStart = false;
	bool isStarted = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.SetTrigger("Chasing");

		startingTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
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

		//Move forward
		//newPosition.x += runningSpeed;
		newPosition.x += speedOverTime.Evaluate((Time.time - startingTime) * 0.1f);

		//Keep on the ground
		float offset = 2.0f;
		Vector3 groundCheck = transform.position;
		groundCheck.y += offset;

		RaycastHit hit = new RaycastHit();
		Physics.Raycast(groundCheck, Vector3.down, out hit);
		newPosition.y -= hit.distance - offset;

		//Get distance to fish
		float distance = target.transform.position.x - newPosition.x;

		//If way behind, catch up
		if (distance > catchupDistance)
		{
			newPosition.x += 10.0f;//= target.transform.position.x - catchupDistance;
		}
		//If ahead of fish, stop
		if(distance < 0)
		{
			newPosition.x -= speedOverTime.Evaluate((Time.time - startingTime) * 0.1f);
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
}
