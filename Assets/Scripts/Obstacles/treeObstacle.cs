using UnityEngine;
using System.Collections;
using System;

public class treeObstacle : ObstacleEvent
{
    public override void bigSizeEvent(GameObject player)
    {
        Debug.Log("Too Big");
    }

    public override void smallSizeEvent(GameObject player)
    {
        Debug.Log("Goes under the tree");
    }

}
