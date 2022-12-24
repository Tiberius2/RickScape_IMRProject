using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class hand_controller : MonoBehaviour
{
    ActionBasedController controller;
    public Hand hand;
    public GameObject room_phone;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hand.PhonePicked && controller.activateAction.action.ReadValue<float>() > 0.2 )
        {
            // drop phone
            hand.phone.SetActive(false);
            room_phone.SetActive(true);
            hand.PhonePicked = false;
            hand.SetRotate(false);
        }

        hand.Action(controller.activateAction.action.ReadValue<float>()); 
    }
}
