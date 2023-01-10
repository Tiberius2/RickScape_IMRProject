using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Behaviour : MonoBehaviour
{
    private string correctSequence;
    private string currentSequence;

    public Material WrongMaterial;
    public Material RightMaterial;
    public Material BaseMaterial;
    public Material[] colourMaterials = new Material[4];
    public GameObject[] BoxWires = new GameObject[4];
    public GameObject FirstWire_Robot;
    public GameObject SecondWire_Robot;
    public ControllerScript controllerScript;

    void Start()
    {
        WireColor.SendColorValue += AddValueAndCheckSequence;
        correctSequence = GenerateSequence();
        currentSequence = "";
    }

    private void OnDestroy()
    {
        WireColor.SendColorValue -= AddValueAndCheckSequence;
    }

    private void AddValueAndCheckSequence(string buttonColor)
    {
        StartCoroutine(Aux(buttonColor));
    }

    private IEnumerator Aux(string buttonColor)
    {
        int element = (int)Enum.Parse(typeof(Colors), buttonColor);
        currentSequence += element;
        Debug.Log(element);
        BoxWires[element - 1].GetComponent<MeshRenderer>().enabled = false;

        Debug.Log("currentSequence: " + currentSequence);
        if (currentSequence != correctSequence.Substring(0, currentSequence.Length))
        {
            currentSequence = "";
            yield return StartCoroutine(FlashColour(WrongMaterial, 2));
            correctSequence = GenerateSequence();
            foreach(var variable in BoxWires)
            {
                variable.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        else if (currentSequence == correctSequence)
        {
            currentSequence = "";
            yield return StartCoroutine(FlashColour(RightMaterial, 2));
            controllerScript.WirePuzzle = true;
        }
    }

    private string GenerateSequence()
    {
        System.Random r;
        string sequence = "";
        string element;

        for (int i = 0; i < 2; ++i)
        {
            r = new System.Random();
            do
            {
                element = r.Next(1, 5).ToString();
            }
            while (sequence.IndexOf(element) != -1);
            sequence += element;
        }

        FirstWire_Robot.GetComponent<Renderer>().material = colourMaterials[sequence[0] - '0' - 1];
        SecondWire_Robot.GetComponent<Renderer>().material = colourMaterials[sequence[1] - '0' - 1];
        gameObject.GetComponent<Renderer>().material = BaseMaterial;
        Debug.Log(sequence);
        return sequence;
    }

    private IEnumerator FlashElement(float time, char c)
    {
        yield return StartCoroutine(FlashColour(colourMaterials[(int)(c - '1')], 1));
        yield return new WaitForSeconds(time);
    }

    private IEnumerator FlashColour(Material material, int time)
    {
        gameObject.GetComponent<Renderer>().material = material;
        yield return StartCoroutine(ResetMaterial(time));
    }

    private IEnumerator ResetMaterial(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Renderer>().material = BaseMaterial;
    }

    public enum Colors
    {
        Yellow = 1,
        Red = 2,
        Blue = 3,
        Green = 4
    }
}
