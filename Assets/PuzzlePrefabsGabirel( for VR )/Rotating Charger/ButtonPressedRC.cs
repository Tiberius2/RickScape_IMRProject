using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressedRC : MonoBehaviour
{
    public RotatingChargerController asd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHand"))
        {
            if (asd.PuzzleEnded == false)
                if (name == "ButtonOutter")
                {
                    StartCoroutine(asd.RotatePiece(asd.OutterWires, 1, 90));
                    StartCoroutine(asd.RotatePiece(asd.MiddleWires, -1, 90));
                }
                else if (name == "ButtonMiddle")
                {
                    StartCoroutine(asd.RotatePiece(asd.OutterWires, 1, 90));
                    StartCoroutine(asd.RotatePiece(asd.InnerWires, -1, 90));
                }
                else if (name == "ButtonInner")
                {
                    StartCoroutine(asd.RotatePiece(asd.InnerWires, -1, 90));
                }
        }
    }
}
