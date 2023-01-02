using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class LeftHandController : MonoBehaviour
{
    ActionBasedController controller;
    public LeftHand hand;
    public GameObject room_flashlight;
    public GameObject room_radio;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
        //hand.SetGrabRadio(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.activateAction.action.ReadValue<float>() > 0.2)
        {
            if (hand.FlashlightPicked)
            {
                // drop flashlight
                hand.flashlight.SetActive(false);
                room_flashlight.SetActive(true);
                hand.FlashlightPicked = false;
                hand.SetGrab(false);
            }
            else if (hand.RadioPicked)
            {
                // drop radio
                hand.radio.gameObject.SetActive(false);
                room_radio.SetActive(true);
                hand.RadioPicked = false;
                hand.SetGrabRadio(false);
            }
        }
        //hand.SetGrab(controller.activateAction.action.ReadValue<float>());
    }    
}
