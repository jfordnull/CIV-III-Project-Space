using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateShip : MonoBehaviour
{
    //A script for activating the ship at the pull of a lever

    public GameObject lightOverhead;
    public GameObject ship;
    public GameObject welcomeVL;
    public GameObject beep;
    private Rigidbody rb;
    private XRBaseInteractable interactable;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        interactable = GetComponent<XRBaseInteractable>();
        interactable.selectExited.AddListener(ShipActivate);
    }

    void ShipActivate(SelectExitEventArgs arg)
    {
        beep.SetActive(true);
        StartCoroutine(lightFlicker());
        StartCoroutine(waitForLever());
    }

    private IEnumerator lightFlicker()
    {
        lightOverhead.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        lightOverhead.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        lightOverhead.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        lightOverhead.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        lightOverhead.SetActive(true);
        welcomeVL.SetActive(true);
    }
    private IEnumerator waitForLever()
    {
        yield return new WaitForSeconds(2f);
        rb.isKinematic = true;
        interactable.enabled = false;
        ship.SetActive(true);
    }
}
