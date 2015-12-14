using UnityEngine;
using System.Collections;



public class ChaserAI : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float moveSpeed = 3;
    [SerializeField]
    private float boost = 15;
    private float resetMoveSpeed;
    //the amount to push down the object if its not grounded
    [SerializeField]
    private float pushDownForce = 100;
    [SerializeField]
    float height = 5.0f;
    [SerializeField]
    float sideForce = 5.0f;
    [SerializeField]
    private float xOffset;
    [SerializeField]
    private float yOffset;
    private bool onGround = true;
    
 

    void Start()
    {
        transform.LookAt(target.transform);
        resetMoveSpeed = moveSpeed;
    }

    void Update()
    {
        checkIfGrounded();

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (!onGround)
        {
            Rigidbody rb = target.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(-sideForce, -pushDownForce));
        }

    }


    //Checks if the object is grounded so, it cannot follow the target upward
    void checkIfGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, height);

        if (hits.Length > 0)
        { 
          onGround = true;
        }
        else
        {
           onGround = false;
        }
    }

    void checkIfStuck()
    {
       Rigidbody rb = target.GetComponent<Rigidbody>();

        if(rb.velocity.magnitude <= 1)
        {
            Respawn();
        }
    }

    //If the object gets the target
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Fisherman catches fish");
        }

        else
        {
            checkIfStuck();
        }
  
    }

    void Respawn()
    {

        Vector3 respawnPositon = new Vector3(target.transform.position.x - xOffset, target.transform.position.y - yOffset);

        transform.position = respawnPositon;
    }
       
        
}
