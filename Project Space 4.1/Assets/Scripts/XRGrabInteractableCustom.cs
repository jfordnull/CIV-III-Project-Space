using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableCustom : XRGrabInteractable
{
    //Just a basic adaptation of the XRGrabInteractable class from Unity's
    //XR Interaction Toolkit. One that allows us to use separate attach points for
    //objects based on which hand is grabbing them

    public Transform leftAttach;
    public Transform rightAttach;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left"))
        {
            attachTransform = leftAttach;
        }
        else if (args.interactorObject.transform.CompareTag("Right"))
        {
            attachTransform = rightAttach;
        }

        base.OnSelectEntered(args);
    }
}
