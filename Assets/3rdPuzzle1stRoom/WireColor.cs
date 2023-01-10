using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WireColor : MonoBehaviour
{
    public static event Action<string> SendColorValue = delegate { };

    public void ButtonClicked()
    {
        Debug.Log("Clicked " + gameObject.name.Substring(0, gameObject.name.IndexOf("_")));
        SendColorValue(gameObject.name.Substring(0, gameObject.name.IndexOf("_")));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("right_hand") || other.gameObject.CompareTag("left_hand"))
        {
            ButtonClicked();
        }
    }
}
