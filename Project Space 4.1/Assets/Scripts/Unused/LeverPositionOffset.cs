using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeverPositionOffset : MonoBehaviour
{
    public Transform anchor;
    private Vector3 offset;
    private Vector3 newOffset;

    // Start is called before the first frame update
    void Start()
    {
        offset = anchor.position - transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        newOffset = anchor.position - transform.position;
        Debug.Log(newOffset - offset);
        if (newOffset != offset)
        {
            transform.position = anchor.position + offset;
        }
        
    }
}
