using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class RigHand : MonoBehaviour
{
    //Readme:
    /*This script snaps the hand which grabs a XRGrabInteractable (Unity XRInteractionToolkit class)
    to a specific pose. The pose data comes from a pre-rigged HandData class object, specifically an array
    of transforms/quaternions for the position/rotation of the hand and each of its bones. (This method was
    inspired by the Youtuber Valem's VR tutorials.) Here we determine which hand is grabbing the object
    and call a listener function for each VR interaction that sets the pose and unsets it by resetting hand bones
    to original rotations.*/


    public HandData rightHandPose;
    public HandData leftHandPose;
    private HandData currentInteractor;
    private Vector3 startingHandPosition;
    private Vector3 finalHandposition;
    private Quaternion[] startingFingerRotations;
    private Quaternion[] finalFingerRotations;
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(EnterPose);
        grabInteractable.selectExited.AddListener(ExitPose);
        rightHandPose.gameObject.SetActive(false);
        leftHandPose.gameObject.SetActive(false);
        grabInteractable.hoverEntered.AddListener(OutlineOn);
        grabInteractable.hoverExited.AddListener(OutlineOff);
        gameObject.GetComponent<Outline>().enabled = false;
    }

    public void OutlineOn(HoverEnterEventArgs arg)
    {
        gameObject.GetComponent<Outline>().enabled = true;
    }

    public void OutlineOff(HoverExitEventArgs arg)
    {
        gameObject.GetComponent<Outline>().enabled = false;
    }

    public void EnterPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            if (arg.interactorObject.transform.CompareTag("Left"))
            {
                currentInteractor = leftHandPose;
            }
            else if (arg.interactorObject.transform.CompareTag("Right"))
            {
                currentInteractor = rightHandPose;
            }
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = false;
            SetHandDataValues(handData, currentInteractor);
            SetHandData(handData, finalHandposition, finalFingerRotations);
        }
    }

    public void ExitPose(BaseInteractionEventArgs arg)
    {
        HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
        handData.animator.enabled = true;
        SetHandData(handData, startingHandPosition, startingFingerRotations);
    }

    public void SetHandDataValues(HandData h1, HandData h2)
    {
        startingHandPosition = new Vector3(h1.root.localPosition.x / h1.root.localScale.x,
            h1.root.localPosition.y / h1.root.localScale.y, h1.root.localPosition.z / h1.root.localScale.z);
        finalHandposition = new Vector3(h2.root.localPosition.x / h2.root.localScale.x,
            h2.root.localPosition.y / h2.root.localScale.y, h2.root.localPosition.z / h2.root.localScale.z);
        startingFingerRotations = new Quaternion[h1.fingerBones.Length];
        finalFingerRotations = new Quaternion[h1.fingerBones.Length];

        for (int i = 0; i < h1.fingerBones.Length; i++)
        {
            startingFingerRotations[i] = h1.fingerBones[i].localRotation;
            finalFingerRotations[i] = h2.fingerBones[i].localRotation;
        }
    }

    public void SetHandData(HandData h, Vector3 newPosition, Quaternion[] newBonesRotations)
    {
        h.root.localPosition = newPosition;

        for (int i = 0; i < newBonesRotations.Length; i++)
        {
            h.fingerBones[i].localRotation = newBonesRotations[i];
        }
    }
}
