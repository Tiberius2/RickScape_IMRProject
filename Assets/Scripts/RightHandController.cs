using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class RightHandController : MonoBehaviour
{
    ActionBasedController controller;
    public RightHand hand;
    public GameObject room_phone;
    public GameObject rightButtonRoom;
    public GameObject leftButtonRoom;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.activateAction.action.ReadValue<float>() > 0.2 )
        {

            if(hand.PhonePicked)
            {
                // drop phone
                hand.phone.SetActive(false);
                room_phone.SetActive(true);
                hand.PhonePicked = false;
                hand.SetRotate(false);
            }
            else if (hand.rightButtonPicked)
            {
                // drop right button
                hand.rightButtonHand.SetActive(false);
                rightButtonRoom.SetActive(true);
                hand.rightButtonPicked = false;
            }
            else if (hand.leftButtonPicked)
            {
                // drop left button
                hand.leftButtonHand.SetActive(false);
                leftButtonRoom.SetActive(true);
                hand.leftButtonPicked = false;
            }
        }

        hand.Action(controller.activateAction.action.ReadValue<float>()); 
    }
}
