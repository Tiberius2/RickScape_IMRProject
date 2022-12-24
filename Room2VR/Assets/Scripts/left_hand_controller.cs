using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class left_hand_controller : MonoBehaviour
{
    ActionBasedController controller;
    public LeftHand hand;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    void Update()
    {
        hand.SetGrab(controller.activateAction.action.ReadValue<float>());
    }

    
}
