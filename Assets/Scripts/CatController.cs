using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

	Animator anim;
	bool pouncing = false;

	GameObject fish;

	[SerializeField]
	float collisionForce = 3.0f;


	// Use this for initialization
	void Awake () {
		anim = GetComponentInChildren<Animator>();
	}
	void Start() {

		fish = GameObject.FindGameObjectWithTag("Player");

	}

	// Update is called once per frame
	void Update () {
		if (pouncing) {
			Vector3 pos = fish.transform.position - transform.position;
			pos.y = 0.0f;
			Quaternion newRot = Quaternion.LookRotation(pos, Vector3.up);
			transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.1f);
		}
	}

	public void Pounce()
	{
		if(anim != null)
			anim.SetTrigger ("Pounce");
		pouncing = true;
	}

	public void Death()
	{
		StartCoroutine(DeathCoroutine());
	}

	IEnumerator DeathCoroutine()
	{
		anim.SetTrigger("Collide");

		GetComponent<AudioSource>().Play();

		transform.SetParent(null);

		pouncing = false;

		Rigidbody rBody = gameObject.AddComponent<Rigidbody>();

		rBody.AddForce(collisionForce, 0f, collisionForce, ForceMode.VelocityChange);

		Manager.singleton.updateCatScore();

		yield return new WaitForSeconds(8.0f);


		GameObject.Destroy(gameObject);

	}

	public void Win()
	{

	}
}
