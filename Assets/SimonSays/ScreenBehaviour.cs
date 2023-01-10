using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenBehaviour : MonoBehaviour
{
    private string correctSequence;
    private string currentSequence;
    private int numberOfIterations;

    public Material WrongMaterial;
    public Material RightMaterial;
    public Material BaseMaterial;
    public Material[] colourMaterials = new Material[4];
    public ControllerScript controllerScript;

    void Start()
    {
        ButtonColor.SendColorValue += AddValueAndCheckSequence;
        correctSequence = GenerateSequence();
        Debug.Log(correctSequence);
        currentSequence = "";
        numberOfIterations = 1;
        StartCoroutine(FlashCorrectSequence());
    }

    private void OnDestroy()
    {
        ButtonColor.SendColorValue -= AddValueAndCheckSequence;
    }

    private void AddValueAndCheckSequence(string buttonColor)
    {
        StartCoroutine(Aux(buttonColor));
    }

    private IEnumerator Aux(string buttonColor)
    {
        int element = (int)Enum.Parse(typeof(Colors), buttonColor);
        currentSequence += element;
        yield return StartCoroutine(FlashColour(colourMaterials[element - 1], 1));

        Debug.Log("currentSequence: " + currentSequence);

        if (currentSequence != correctSequence.Substring(0, currentSequence.Length))
        {
            currentSequence = "";
            yield return StartCoroutine(FlashColour(WrongMaterial, 2));
            yield return StartCoroutine(FlashCorrectSequence());
            numberOfIterations = 3;
        }
        else if (currentSequence == correctSequence)
        {
            currentSequence = "";
            yield return StartCoroutine(FlashColour(RightMaterial, 2));
            //correctSequence = GenerateSequence();
            //Debug.Log("New correct sequence: " + correctSequence);
            //yield return StartCoroutine(FlashCorrectSequence());
            --numberOfIterations;
            if(numberOfIterations== 0)
            {
                // puzzle solved.
                Debug.Log("Done");
                controllerScript.SimonSays = true;
            }
        }
    }

    private string GenerateSequence()
    {
        System.Random r;
        string sequence = "";

        for (int i = 0; i < 4; ++i)
        {
            r = new System.Random();
            sequence += r.Next(1, 5).ToString();
        }
        return sequence;
    }

    private IEnumerator FlashCorrectSequence()
    {
        foreach (char c in correctSequence)
        {
            yield return StartCoroutine(FlashElement(1, c));
        }
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
}

public enum Colors
{
    Purple = 1,
    Orange = 2,
    Blue = 3,
    Yellow = 4
}
