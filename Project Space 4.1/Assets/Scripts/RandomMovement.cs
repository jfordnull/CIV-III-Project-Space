using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    //README:
    /*Applies random thrust and rotation to the rigidbody of each individual asteroid.*/

    public float min_spinSpeed = 1f;
    public float max_spinSpeed = 5f;
    public float minThrust = 0.1f;
    public float maxThrust = 0.5f;
    private float spinSpeed;
    void Start()
    {
        spinSpeed = Random.Range(min_spinSpeed, max_spinSpeed);
        float thrustx = Random.Range(minThrust, maxThrust);
        float thrusty = Random.Range(minThrust, maxThrust);
        float thrustz = Random.Range(minThrust, maxThrust);
        Rigidbody rg = GetComponent<Rigidbody>();
        rg.AddForce(new Vector3(thrustx,thrusty,thrustz), ForceMode.Impulse);
    }
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
