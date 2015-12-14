using UnityEngine;
using System.Collections;



public class ChaserAI : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float moveSpeed = 3;
    //the amount to push down the object if its not grounded
    [SerializeField]
    private float pushDownForce = 100;
    [SerializeField]
    float height = 5.0f;
    [SerializeField]
    private float yOffset;
    [SerializeField]
    private float secondsTilRespawn;
    private bool onGround = true;
    private bool following = true;
    [SerializeField]
    private GameObject chaser;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float minDistance;
    private Rigidbody rbAI;
    private Animator chaserAnimation;
    private bool respawing = false;
    private GameObject chaserPrefab;
    
 

    void Start()
    {
        rbAI = chaser.GetComponent<Rigidbody>();
        chaserAnimation = chaser.GetComponent<Animator>();
        transform.LookAt(target.transform);
        chaserAnimation.SetTrigger("Chasing");
    }

    void Update()
    {
        checkIfTooFar();
    }

    void FixedUpdate()
    {
        checkIfGrounded();
        

        if (following)
        {
            //Chaser needs to have interpolate ON in the rigidbody for a smooth transition
            rbAI.MovePosition(Vector3.MoveTowards(chaser.transform.position, target.transform.position, moveSpeed * Time.deltaTime));
 
        }

        //Pushes the object down so, it doesn't move with the targer upward
        if (!onGround)
        {
            rbAI.AddForce(new Vector3(0,-pushDownForce,0));
        }
    }

    void checkIfTooFar()
    {
        float distance = Vector3.Distance(rbAI.position, target.transform.position);
        if(distance > maxDistance || distance < minDistance && !respawing)
        {
            Debug.Log("Too Far Away from target, respawn started");
            chaserAnimation.SetTrigger("Idle");
            respawing = true;
            rbAI.velocity = Vector3.zero;
            StartCoroutine(Respawn());
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


    //If the object gets the target. If the object is blocked by a tree than a respawn occurs. 
    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            //Death method start here
            Debug.Log("Chef catches fish");
            chaserAnimation.SetTrigger("KnifeWave");
        }

        //Respawns the chaser
        else if (col.gameObject.tag == "Tree" && !respawing)
        {
            Debug.Log("Hit a tree, respawn started");
            rbAI.velocity = Vector3.zero;
            chaserAnimation.SetTrigger("Idle");
            following = false;
            respawing = true;
            StartCoroutine(Respawn());
        }

    }

    //Respawns the AI after a certain amount of seconds
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(secondsTilRespawn);
        GameObject.Destroy(chaser);
        //Creates a respawn location with the target location with an offset so, it will not be on top of the target
        Vector3 respawnPositon = new Vector3(target.transform.position.x, target.transform.position.y + yOffset);
        Instantiate(chaser, respawnPositon, Quaternion.identity);
        //rbAI.position = respawnPositon;
        following = true;
        respawing = false;
        chaserAnimation.SetTrigger("Chasing");

    }
       
        
}
