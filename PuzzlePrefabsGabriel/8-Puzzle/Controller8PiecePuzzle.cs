using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class Controller8PiecePuzzle : MonoBehaviour
{
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    public GameObject position4;
    public GameObject position5;
    public GameObject position6;
    public GameObject position7;
    public GameObject position8;
    public GameObject position9;

    public GameObject Piece1;
    public GameObject Piece2;
    public GameObject Piece3;
    public GameObject Piece4;
    public GameObject Piece5;
    public GameObject Piece6;
    public GameObject Piece7;
    public GameObject Piece8;

    private float minDistance = 0.1f;
    public bool movePiece = false;
    public bool puzzleEnded = false;
    public RaycastHit hit;
    public Vector3 endPosition;
    private async void AttemptMovement(GameObject piece)
    {
        if (piece.GetComponent<NearPieces>().up_open)
        {
            if (Physics.Raycast(piece.transform.position, piece.transform.forward, out hit))
            {
                if(hit.collider.CompareTag("position"))
                {
                    endPosition = hit.collider.gameObject.transform.position;
                    movePiece = true;
                    await MovePiece(piece, endPosition, 3f);
                }
            }

        }
        else if (piece.GetComponent<NearPieces>().down_open)
        {
            if (Physics.Raycast(piece.transform.position, -piece.transform.forward, out hit))
            {
                if (hit.collider.CompareTag("position"))
                {
                    endPosition = hit.collider.gameObject.transform.position;
                    movePiece = true;
                    await MovePiece(piece, endPosition, 3f);
                }
            }

        }
        else if (piece.GetComponent<NearPieces>().left_open)
        {
            if (Physics.Raycast(piece.transform.position, -piece.transform.up, out hit))
            {
                if (hit.collider.CompareTag("position"))
                {
                    endPosition = hit.collider.gameObject.transform.position;
                    movePiece = true;
                    await MovePiece(piece, endPosition, 3f);
                }
            }

        }
        else if (piece.GetComponent<NearPieces>().right_open)
        {
            if (Physics.Raycast(piece.transform.position, piece.transform.up, out hit))
            {
                if (hit.collider.CompareTag("position"))
                {
                    endPosition = hit.collider.gameObject.transform.position;
                    movePiece = true;
                    await MovePiece(piece, endPosition, 3f);
                }
            }

        }
    }

    private async Task MovePiece(GameObject piece, Vector3 destination, float time)
    {
        float elapsedTime = 0;
        float distance = 0;
        while (movePiece == true)
        {
            distance = Vector3.Distance(piece.transform.position, destination);
            piece.transform.position = Vector3.Lerp(piece.transform.position, destination, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            if (distance < minDistance)
            {
                movePiece = false;
            }
            await Task.Yield();
        }
    }

    private void checkPuzzle()
    {
        if (position1.GetComponent<CollidingWith>().InMyPosition == "Piece1")
            if (position2.GetComponent<CollidingWith>().InMyPosition == "Piece2")
                if (position3.GetComponent<CollidingWith>().InMyPosition == "Piece3")
                    if (position4.GetComponent<CollidingWith>().InMyPosition == "Piece4")
                        if (position5.GetComponent<CollidingWith>().InMyPosition == "Piece5")
                            if (position6.GetComponent<CollidingWith>().InMyPosition == "Piece6")
                                if (position7.GetComponent<CollidingWith>().InMyPosition == "Piece7")
                                    if (position8.GetComponent<CollidingWith>().InMyPosition == "Piece8")
                                        puzzleEnded = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (movePiece == false && puzzleEnded ==false)
        {
            checkPuzzle();

            if (Input.GetKey("1"))
            {
                AttemptMovement(Piece1);
            }
            if (Input.GetKey("2"))
            {
                AttemptMovement(Piece2);
            }
            if (Input.GetKey("3"))
            {
                AttemptMovement(Piece3);
            }
            if (Input.GetKey("4"))
            {
                AttemptMovement(Piece4);
            }
            if (Input.GetKey("5"))
            {
                AttemptMovement(Piece5);
            }
            if (Input.GetKey("6"))
            {
                AttemptMovement(Piece6);
            }
            if (Input.GetKey("7"))
            {
                AttemptMovement(Piece7);
            }
            if (Input.GetKey("8"))
            {
                AttemptMovement(Piece8);
            }
        }
    }
}
