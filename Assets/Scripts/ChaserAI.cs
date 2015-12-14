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
    private Rigidbody rbAI;
    
 

    void Start()
    {
        rbAI = chaser.GetComponent<Rigidbody>();
        transform.LookAt(target.transform);
    }

    void FixedUpdate()
    {
        checkIfGrounded();

        if (following)
        {
            rbAI.MovePosition(Vector3.MoveTowards(chaser.transform.position, target.transform.position, moveSpeed * Time.deltaTime));
        }

        //Pushes the object down so, it doesn't move with the targer upward
        if (!onGround)
        {
            rbAI.AddForce(new Vector3(0,-pushDownForce,0));
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
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            //Death method start here
            Debug.Log("Fisherman catches fish");
        }

        //Respawns the chaser
        else if (col.gameObject.tag == "Tree")
        {
            rbAI.velocity = Vector3.zero;
            following = false;
            StartCoroutine(Respawn());
        }

    }

    //Respawns the AI after a certain amount of seconds
    IEnumerator Respawn()
    {
        Debug.Log("Respawn started");
        yield return new WaitForSeconds(secondsTilRespawn);
        //Creates a respawn location with the target location with an offset so, it will not be on top of the target
        Vector3 respawnPositon = new Vector3(target.transform.position.x, target.transform.position.y + yOffset);
        rbAI.position = respawnPositon;
        following = true;

    }
       
        
}
