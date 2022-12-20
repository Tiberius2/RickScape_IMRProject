using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        OutterGood = false;
        MiddleGood = false;
        InnerGood = false;
        PuzzleEnded = false;
    }

    private IEnumerator RotatePiece(GameObject piece, float left, float degrees)
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
        Battery.AddComponent<Rigidbody>();
        Battery.GetComponent<Rigidbody>().AddForce(Battery.transform.forward * ejectForce);
    }

    void Update()
    {
        CheckEnd();
        if (PuzzleEnded == false)
        {
            if (Input.GetKeyDown("j"))
            {
                StartCoroutine(RotatePiece(OutterWires, 1, 90));
                StartCoroutine(RotatePiece(MiddleWires, -1, 90));
                //MiddleWires.transform.Rotate(-90, 0, 0);
            }
            if (Input.GetKeyDown("k"))
            {
                StartCoroutine(RotatePiece(OutterWires, 1, 90));
                //MiddleWires.transform.Rotate(90, 0, 0);
                StartCoroutine(RotatePiece(InnerWires, -1, 90));
                //InnerWires.transform.Rotate(-90, 0, 0);
            }
            if (Input.GetKeyDown("l"))
            {
                StartCoroutine(RotatePiece(InnerWires, -1, 90));
                //InnerWires.transform.Rotate(90, 0, 0);
            }
        }
        if (PuzzleEnded == true)
        {
            EjectBattery();
        }
    }

}
