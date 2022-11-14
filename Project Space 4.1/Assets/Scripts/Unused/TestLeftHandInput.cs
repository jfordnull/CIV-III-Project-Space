using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestLeftHandInput : MonoBehaviour
{
    public InputActionProperty testaction;
    public InputActionProperty testaction2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = testaction.action.ReadValue<float>();
        Vector2 joystickValue = testaction2.action.ReadValue<Vector2>();
        Debug.Log(joystickValue.ToString());
    }
}
