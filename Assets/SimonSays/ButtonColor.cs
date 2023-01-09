using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonColor : MonoBehaviour
{
    public static event Action<string> SendColorValue = delegate { };

    public void ButtonClicked()
    {
        Debug.Log("Clicked " + gameObject.name.Substring(0, gameObject.name.IndexOf("_")));
        SendColorValue(gameObject.name.Substring(0, gameObject.name.IndexOf("_")));
    }
    
    private void OnMouseDown()
    {
        ButtonClicked();
    }
}
