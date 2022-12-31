using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearPieces : MonoBehaviour
{
    public bool up_open;
    public bool down_open;
    public bool left_open;
    public bool right_open;
    public float checkDistance = 5f;
    public LayerMask anotherPiece;
    public Controller8PiecePuzzle ControllerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (ControllerScript.movePiece == false && ControllerScript.puzzleEnded == false)
        {
            if (other.gameObject.CompareTag("PlayerHand"))
            {
                if (name == "Piece1")
                {
                    ControllerScript.AttemptMovement(ControllerScript.Piece1);
                }
                else if (name == "Piece2")
                {
                    ControllerScript.AttemptMovement(ControllerScript.Piece2);
                }
                else if (name == "Piece3")
                {
                    ControllerScript.AttemptMovement(ControllerScript.Piece3);
                }
                else if (name == "Piece4")
                {
                    ControllerScript.AttemptMovement(ControllerScript.Piece4);
                }
                else if (name == "Piece5")
                {
                    ControllerScript.AttemptMovement(ControllerScript.Piece5);
                }
                else if (name == "Piece6")
                {
                    ControllerScript.AttemptMovement(ControllerScript.Piece6);
                }
                else if (name == "Piece7")
                {
                    ControllerScript.AttemptMovement(ControllerScript.Piece7);
                }
                else if (name == "Piece8")
                {
                    ControllerScript.AttemptMovement(ControllerScript.Piece8);
                }
            }
        }
    }
    void Update()
    {
        up_open = !Physics.Raycast(this.transform.position, this.transform.forward, checkDistance, anotherPiece);
        down_open = !Physics.Raycast(this.transform.position, -this.transform.forward, checkDistance, anotherPiece);
        right_open = !Physics.Raycast(this.transform.position, this.transform.up, checkDistance, anotherPiece);
        left_open = !Physics.Raycast(this.transform.position, -this.transform.up, checkDistance, anotherPiece);


    }
}
