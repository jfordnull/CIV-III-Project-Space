using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityDamper : MonoBehaviour
{
    //This script limits the max velocity of the Interactables.

    public float maxVel = 1.0f;
    public float maxAngVel = 1.0f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        Vector3 angVel = rb.angularVelocity;
        if (vel.magnitude > maxVel)
        {
            rb.velocity = vel.normalized * maxVel;
        }
        if (angVel.magnitude > maxAngVel)
        {
            rb.angularVelocity = angVel.normalized * maxAngVel;
        }
    }
}
