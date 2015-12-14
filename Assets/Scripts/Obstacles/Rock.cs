using UnityEngine;
using System.Collections;

public class Rock : ObstacleEvent
{
    //destory rock and have a animation?
    public override void bigSizeEvent(GameObject player)
    {
        Destroy(gameObject);
    }

    //Maybe a death method
    public override void smallSizeEvent(GameObject player)
    {
        Destroy(player);
        Debug.Log("Too small");
    }

	
}
