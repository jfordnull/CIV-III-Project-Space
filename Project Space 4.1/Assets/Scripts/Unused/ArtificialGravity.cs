using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialGravity : MonoBehaviour
{
    public Transform floorPlane;
    public float gravity;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float difference = transform.position.y - floorPlane.position.y;
    }
}
