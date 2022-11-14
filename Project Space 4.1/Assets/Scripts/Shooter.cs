using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Shooter : MonoBehaviour
{
    /*This script handles shooting and the "rigging" of the hand while the shooter Interactable
     is in use.*/

    public GameObject rightHand;
    public GameObject reticleDistance;
    public GameObject turret;
    private Outline outline;
    public GameObject explosion;
    public AudioSource explosionSource;
    public AudioClip expls;
    public AudioClip lzer;
    private IEnumerator shoot;
    RaycastHit hit;
    public GameObject vrHandMesh;
    public GameObject reticle;
    public InputActionProperty shootButton;
    public GameObject shooterOrigin;
    private bool inUse;
    Ray ray;
    public TrailRenderer laserEffect;

    //Declaring listerner functions, grabbing the XRSimpleInteractable, etc:
    void Start()
    {
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(ShowHand);
        interactable.selectExited.AddListener(UnshowHand);
        rightHand.gameObject.SetActive(false);
        reticle.gameObject.SetActive(false);
        interactable.hoverEntered.AddListener(OutlineOn);
        interactable.hoverExited.AddListener(OutlineOff);
        gameObject.GetComponent<Outline>().enabled = false;
        inUse = false;
        shoot = Shoot();
    }

    /*Shooting is handled by calling a coroutine while the shooter Interactable is currently activated/grabbed.
     A raycast is drawn from an empty gameobject, shooterOrigin (positioned near the nose of the ship). If that
    raycast makes contact with an asteroid, it passes information about the target to another coroutine below
    which handles the outline. If the shoot button is pressed, a trail renderer draws a laser from the turret of
    the ship to the target (either the object the ray collided with or a pre-defined value in front of the ship
    just past the reticle).Also plays sound effects, spawns a particle system of debris, and destroys target gameobject.*/
    private IEnumerator Shoot()
    {
        while (true)
        {
            float shootPressed;
            ray.origin = shooterOrigin.transform.position;
            ray.direction = shooterOrigin.transform.forward;
            if (Physics.Raycast(ray, out hit))
            {
                StartCoroutine(Outline(ray, hit, hit.transform.gameObject));
            }
            shootPressed = shootButton.action.ReadValue<float>();
            if (shootPressed > 0)
            {
                TrailRenderer tracer = Instantiate(laserEffect, turret.transform.position, Quaternion.identity);
                tracer.AddPosition(turret.transform.position);
                if (Physics.Raycast(ray, out hit))
                {
                    Destroy(hit.transform.gameObject);
                    Instantiate(explosion, hit.point, Quaternion.identity);
                    explosionSource.PlayOneShot(expls, 0.5f);
                    explosionSource.PlayOneShot(lzer, 0.7f);
                    tracer.transform.position = hit.point;
                }
                else
                {
                    explosionSource.PlayOneShot(lzer, 0.7f);
                    tracer.transform.position = reticleDistance.transform.position;
                }
            }
            yield return null;
        }
    }

    //Handling the outline for targetted asteroids:

    private IEnumerator Outline(Ray ray, RaycastHit hit, GameObject asteroid)
    {
        outline = asteroid.GetComponent<Outline>();
        if ((Physics.Raycast(ray, out hit)) && hit.transform.gameObject == asteroid)
        {
            
            outline.enabled = true;
            yield return null;
        }
        else
        {
            outline.enabled = false;
            yield break;
        }
    }

    //Triggering the outline script on hover:

    public void OutlineOn(HoverEnterEventArgs arg)
    {
        gameObject.GetComponent<Outline>().enabled = true;
    }

    public void OutlineOff(HoverExitEventArgs arg)
    {
        gameObject.GetComponent<Outline>().enabled = false;
    }

    //Showing a prerigged hand for the shooter and hiding the movable/VR player hand:

    public void UnshowHand(SelectExitEventArgs arg)
    {
        if (inUse)
        {
            vrHandMesh.gameObject.SetActive(true);
            rightHand.gameObject.SetActive(false);
            reticle.gameObject.SetActive(false);
            StopCoroutine(shoot);
            inUse = false;
        }

    }
    public void ShowHand(SelectEnterEventArgs arg)
    {
        rightHand.gameObject.SetActive(true);
        vrHandMesh.gameObject.SetActive(false);
        reticle.gameObject.SetActive(true);
        StartCoroutine(shoot);
        inUse = true;
    }
}
