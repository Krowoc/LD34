using UnityEngine;
using System.Collections;

public class BeehiveController : MonoBehaviour {

	public GameObject bees;
	Rigidbody rBody;

	float startingZ;

	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody>();
		PhysicsEnabled(false);

		rBody.freezeRotation = true;

		startingZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newPosition = transform.position;

		newPosition.z = startingZ;

		transform.position = newPosition;
	}

	void OnTriggerEnter(Collider other)
	{
		FishController fish = other.GetComponent<FishController>();

		if (fish != null)
		{
			PhysicsEnabled(true);
			GameObject beePrefab = Instantiate(bees);
			beePrefab.transform.position = transform.position;

		}

		ChefController chef = other.GetComponent<ChefController>();

		if (chef != null)
		{
			chef.HitBeehive();
			StartCoroutine(Despawn(2.0f));

		}
	}

	void PhysicsEnabled(bool enabled)
	{
		rBody.useGravity = enabled;

	}

	IEnumerator Despawn(float delay)
	{
		Debug.Log("Destroy");
		yield return new WaitForSeconds(delay);

		//TODO: Add fading effect

		Destroy(gameObject);
	}
}
