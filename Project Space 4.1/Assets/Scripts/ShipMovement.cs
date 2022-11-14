using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class ShipMovement : MonoBehaviour
{

    /*This script handles the movement of the ship mover's rigidbody. (Ship mover is 
     an empty gameobject that the ship itself tracks, to avoid unintended physics issues with
    collision boxes inside of collision boxes.)*/

    public InputActionProperty leftStick;
    public InputActionProperty rightStick;
    public InputActionProperty grip;
    public float timeleftBoost;
    public float leverAngleThreshold;
    //public XRGrabInteractable lever;


    [Header("=== Ship Movement Settings ===")]
    [SerializeField]
    private float yawTorque = 500f;
    [SerializeField]
    private float pitchTorque = 1000f;
    [SerializeField]
    private float rollTorque = 1000f;
    [SerializeField]
    private float thrust = 100f;
    [SerializeField]
    private float upThrust = 50f;
    [SerializeField]
    private float strafeThurst = 50f;


    [SerializeField, Range(0.001f, 0.999f)]
    private float thrustGlideReduction = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float upDownGlideRedution = 0.111f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float leftRightGlideRedution = 0.111f;
    float glide, verticalGlide, horizontalGlide = 0f;

    Rigidbody rb;

    //Input Values
    private float thrust1D;
    private float upDown1D;
    private float strafe1D;
    private float roll1D;
    private Vector2 pitchYaw;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 leftAxis = leftStick.action.ReadValue<Vector2>();
        thrust1D = leftAxis.y;
        strafe1D = leftAxis.x;
        pitchYaw = rightStick.action.ReadValue<Vector2>();
        roll1D = grip.action.ReadValue<float>();
        HandleMovement();

    }

    void HandleMovement()
    {

        // Roll
        rb.AddRelativeTorque(Vector3.back * roll1D * rollTorque * Time.deltaTime);
        // Pitch
        rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(-pitchYaw.y, -1f, 1f) * pitchTorque * Time.deltaTime);
        // Yaw
        rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(pitchYaw.x, -1f, 1f) * yawTorque * Time.deltaTime);


        // Thrust
        if (thrust1D > 0.1f || thrust1D < -0.1f)
        {
            float currentThrust;

            currentThrust = thrust;


            rb.AddRelativeForce(Vector3.forward * thrust1D * currentThrust * Time.deltaTime);
            glide = thrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.forward * glide * Time.deltaTime);
            glide *= thrustGlideReduction;
        }

        // UP/DOWN

        if (upDown1D > 0.1f || upDown1D < -0.1f)
        {
            rb.AddRelativeForce(Vector3.up * upDown1D * upThrust * Time.fixedDeltaTime);
            verticalGlide = upDown1D * upThrust;

        }
        else
        {
            rb.AddRelativeForce(Vector3.up * verticalGlide * Time.fixedDeltaTime);
            verticalGlide *= upDownGlideRedution;
        }

        // STRAFTING

        if (strafe1D > 0.1f || strafe1D < -0.1f)
        {
            rb.AddRelativeForce(Vector3.right * strafe1D * upThrust * Time.fixedDeltaTime);
            horizontalGlide = strafe1D * strafeThurst;
        }
        else
        {
            rb.AddRelativeForce(Vector3.right * horizontalGlide * Time.fixedDeltaTime);
            horizontalGlide *= leftRightGlideRedution;
        }
    }
}
