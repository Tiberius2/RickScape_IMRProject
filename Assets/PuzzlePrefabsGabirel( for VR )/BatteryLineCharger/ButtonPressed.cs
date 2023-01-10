using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    public bool pressed = false;
    public BatteryChargerController ControllerScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("right_hand") || other.gameObject.CompareTag("left_hand"))
        {
            if (ControllerScript.movePiece == false && ControllerScript.puzzleEnded == false)
            {
                if (name == "ButtonLeft")
                {
                    if (ControllerScript.PuzzlePositions[1] == "Empty" ^ ControllerScript.RetractedPositions[0] == "Empty")
                    {
                        ControllerScript.movePiece = true;
                        if (ControllerScript.PuzzlePositions[1] == "Empty")
                        {
                            ControllerScript.endPosition = ControllerScript.Position_2.transform.position;
                            ControllerScript.PieceToMove = ControllerScript.Retracted_1.GetComponent<CollidingWith>().ObjectInMyPosition;
                        }
                        else
                        {
                            ControllerScript.endPosition = ControllerScript.Retracted_1.transform.position;
                            ControllerScript.PieceToMove = ControllerScript.Position_2.GetComponent<CollidingWith>().ObjectInMyPosition;
                        }

                        StartCoroutine(ControllerScript.SmoothLerp(ControllerScript.PieceToMove, ControllerScript.endPosition, 3f));
                    }
                }
                else if (name == "ButtonMiddle")
                {
                    ControllerScript.MoveLeftRight();
                }


                else if (name == "ButtonRight")
                {
                    if (ControllerScript.PuzzlePositions[3] == "Empty" ^ ControllerScript.RetractedPositions[1] == "Empty")
                    {
                        if (ControllerScript.PuzzlePositions[3] == "Empty")
                        {
                            ControllerScript.endPosition = ControllerScript.Position_4.transform.position;
                            ControllerScript.PieceToMove = ControllerScript.Retracted_2.GetComponent<CollidingWith>().ObjectInMyPosition;
                        }
                        else
                        {
                            ControllerScript.endPosition = ControllerScript.Retracted_2.transform.position;
                            ControllerScript.PieceToMove = ControllerScript.Position_4.GetComponent<CollidingWith>().ObjectInMyPosition;
                        }
                        ControllerScript.movePiece = true;
                        StartCoroutine(ControllerScript.SmoothLerp(ControllerScript.PieceToMove, ControllerScript.endPosition, 3f));
                    }
                }
            }
        }
    }

}
