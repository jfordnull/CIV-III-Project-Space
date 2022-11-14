using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandData : MonoBehaviour
{
    /*This class is used to store an array of transforms for the finger bones
     of a hand, which is used in the RigHand script to set custom poses.*/
    public enum HandModel { Left, Right};
    public HandModel model;
    public Transform root;
    public Animator animator;
    public Transform[] fingerBones;
}
