using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMovement : MonoBehaviour
{
    /*This script is simply for having one object match the position of another.
     (Specifically in situations where the follower object can't be a child of the 
    object we're tracking.)*/

    public Transform followObject;
    void FixedUpdate()
    {
        transform.position = followObject.position;
        transform.rotation = followObject.rotation;
    }
}
