using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    public Transform floor;
    public float gravity = 1f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 gravityUp = Vector3.zero;
        gravityUp = floor.transform.up;

        rb.AddForce((-gravityUp * gravity) * rb.mass);
    }
}
