using UnityEngine;
using System.Collections;

public abstract class ObstacleEvent : MonoBehaviour
{
    [SerializeField]
    //Used to check if the object is big or small
    private float bigTargetScale = 1.0f;

    public void OnTriggerEnter(Collider col)
    {
        //Checks if its big. If so, do the method for the big fish
        if (col.gameObject.transform.localScale.x > bigTargetScale)
        {
            bigSizeEvent(col.GetComponent<GameObject>());
        }

        //Do the method for when the fish is small
        else
        {
            smallSizeEvent(col.GetComponent<GameObject>());
        }
    }

    //Grabs gameobject to effect the fish in different ways
    public abstract void bigSizeEvent(GameObject player);
    public abstract void smallSizeEvent(GameObject player);

	
}
