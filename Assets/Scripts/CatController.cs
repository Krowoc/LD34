using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

	Animator anim;
	Rigidbody rBody;

	[SerializeField]
	float collisionForce = 3.0f;


	// Use this for initialization
	void Awake () {
		anim = GetComponentInChildren<Animator>();
		rBody = GetComponentInChildren<Rigidbody>();
		rBody.detectCollisions = false;
		rBody.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Pounce()
	{
		anim.SetTrigger("Pounce");

	}

	public void Death()
	{
		StartCoroutine(DeathCoroutine());
	}

	IEnumerator DeathCoroutine()
	{
		anim.SetTrigger("Collide");
		rBody.gameObject.transform.SetParent(null);
		rBody.detectCollisions = true;
		rBody.useGravity = true;
		rBody.AddForce(collisionForce, 0f, collisionForce, ForceMode.VelocityChange);
		

		yield return new WaitForSeconds(5.0f);

		GameObject.Destroy(gameObject);
	}
}
