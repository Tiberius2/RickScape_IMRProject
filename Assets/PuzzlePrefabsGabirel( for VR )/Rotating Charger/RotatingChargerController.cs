using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RotatingChargerController : MonoBehaviour
{

    public GameObject OutterWires;
    public GameObject MiddleWires;
    public GameObject InnerWires;
    public GameObject Battery;

    public LayerMask correct;
    public bool OutterGood;
    public bool MiddleGood;
    public bool InnerGood;
    public bool PuzzleEnded;
    public float ejectForce = 10f;
    public ControllerScript controllerScript;

    void Start()
    {
        OutterGood = false;
        MiddleGood = false;
        InnerGood = false;
        PuzzleEnded = false;
    }

    public IEnumerator RotatePiece(GameObject piece, float left, float degrees)
    {
        for (float i =0; i<degrees/5; i+= 1f)
        {
            piece.transform.Rotate(5 * left, 0, 0);
            yield return null;
        }

    }

    private void CheckEnd()
    {
        OutterGood = Physics.Raycast(OutterWires.transform.position, OutterWires.transform.forward, 50f, correct);
        MiddleGood = Physics.Raycast(MiddleWires.transform.position, MiddleWires.transform.forward, 50f, correct);
        InnerGood = Physics.Raycast(InnerWires.transform.position, InnerWires.transform.forward, 50f, correct);
        if(OutterGood && MiddleGood && InnerGood)
        {
            PuzzleEnded = true;
        }
        else
        {
            PuzzleEnded = false;
        }
    }

    private void EjectBattery()
    {
        Battery.GetComponent<Rigidbody>().useGravity = true;
        Battery.GetComponent<Rigidbody>().AddForce(Battery.transform.right * ejectForce);
        Battery.GetComponent<XRGrabInteractable>().enabled = true;
    }

    void Update()
    {
        CheckEnd();

        if (PuzzleEnded == true)
        {
            controllerScript.RoundPuzzle = true;
            EjectBattery();
        }
    }

}
