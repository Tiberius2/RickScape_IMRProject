using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSuit : MonoBehaviour
{
    public ControllerScript controllerScript;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        if (collision.other.CompareTag("right_hand") || collision.other.CompareTag("left_hand"))
        {
            controllerScript.Ending1 = true;
        }
    }
}
