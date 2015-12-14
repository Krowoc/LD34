using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

	Animator anim;
	
	[SerializeField]
	float collisionForce = 3.0f;


	// Use this for initialization
	void Awake () {
		anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Pounce()
	{
		if(anim != null)
			anim.SetTrigger("Pounce");

	}

	public void Death()
	{
		StartCoroutine(DeathCoroutine());
	}

	IEnumerator DeathCoroutine()
	{
		anim.SetTrigger("Collide");

		transform.SetParent(null);

		Rigidbody rBody = gameObject.AddComponent<Rigidbody>();

		rBody.AddForce(collisionForce, 0f, collisionForce, ForceMode.VelocityChange);
		

		yield return new WaitForSeconds(8.0f);

		GameObject.Destroy(gameObject);

		/*Destroy(rBody);

		//Vector3 newPosition = transform.parent.parent.parent.position;

		//newPosition.z = 0.0f;

		transform.rotation = Quaternion.Euler(Vector3.zero);

		transform.parent.parent.parent.position = Vector3.zero; //newPosition;

		anim.SetTrigger("Idle");*/
	}
}
