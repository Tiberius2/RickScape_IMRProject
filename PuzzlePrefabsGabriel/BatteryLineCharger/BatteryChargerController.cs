using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
public class BatteryChargerController : MonoBehaviour
{

    public List<string> PuzzlePositions;
    public List<string> RetractedPositions;
    public bool GoLeft;
    public int nextPosition;

    public GameObject Position_1;
    public GameObject Position_2;
    public GameObject Position_3;
    public GameObject Position_4;
    public GameObject Position_5;
    public GameObject Retracted_1;
    public GameObject Retracted_2;
    public GameObject PositiveCharger;
    public GameObject Battery;
    public GameObject NegativeCharger;
    public GameObject[] Positions_array;
    public GameObject InsertExit_Slot;

    public Vector3 endPosition;
    public float speed = 5f;
    public float ejectForce = 10f;
    public bool movePiece = false;
    public bool puzzleEnded = false;
    public GameObject PieceToMove;
    public float distance;
    private float minDistance = 0.1f;
    

    void Start()
    {
        Positions_array = new GameObject[5] { Position_1, Position_2, Position_3, Position_4, Position_5 };

        GoLeft = false;
        puzzleEnded = false;
    }

    private void EjectBattery()
    {
        Battery.GetComponent<Rigidbody>().useGravity = true;
        Battery.GetComponent<Rigidbody>().AddForce(Battery.transform.forward * ejectForce);
    }

    private IEnumerator SmoothLerp(GameObject piece, Vector3 destination, float time)
    {
        float elapsedTime = 0;

        while (movePiece == true || puzzleEnded ==true)
        {
            distance = Vector3.Distance(piece.transform.position, destination);
            Debug.Log(PieceToMove);
            Debug.Log(distance);
            piece.transform.position = Vector3.Lerp(piece.transform.position, destination, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
            if (distance < minDistance && movePiece ==true)
            {
                movePiece = false;
            }
            if( distance == 0 && puzzleEnded == true)
            {
                break;
            }
        }
    }

    private IEnumerator EndPuzzleMovement()
    {
        StartCoroutine(SmoothLerp(PositiveCharger, Position_2.transform.position, 3f));
        StartCoroutine(SmoothLerp(Battery, Position_3.transform.position, 3f));
        StartCoroutine(SmoothLerp(NegativeCharger, Position_4.transform.position, 3f));
        yield return StartCoroutine(Waiting(3f));
        yield return StartCoroutine(SmoothLerp(Battery, InsertExit_Slot.transform.position, 3f));
        EjectBattery();
    }
    private IEnumerator Waiting(float t)
    {
        yield return new WaitForSeconds(t);
    }

    private async Task MovingObject(GameObject piece, Vector3 destination, float time)
    {
        float elapsedTime = 0;

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

    private async void MoveLeftRight()
    {
        {
            if (GoLeft == true)
            {
                GoLeft = false;
                for (int i = 1; i < 5; i++)
                {
                    if (Positions_array[i].GetComponent<CollidingWith>().InMyPosition != "Empty")
                    {
                        nextPosition = i;
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (Positions_array[j].GetComponent<CollidingWith>().InMyPosition == "Empty")
                            {
                                nextPosition = j;
                            }
                        }
                        endPosition = Positions_array[nextPosition].transform.position;
                        PieceToMove = Positions_array[i].GetComponent<CollidingWith>().ObjectInMyPosition;
                        Debug.Log("Moving object" + PieceToMove);
                        movePiece = true;
                        await MovingObject(PieceToMove, endPosition, 3f);
                    }

                }
            }
            else if (GoLeft == false)
            {
                GoLeft = true;
                for (int i = 3; i > -1; i--)
                {
                    if (Positions_array[i].GetComponent<CollidingWith>().InMyPosition != "Empty")
                    {
                        nextPosition = i;
                        for (int j = i + 1; j <= 4; j++)
                        {
                            if (Positions_array[j].GetComponent<CollidingWith>().InMyPosition == "Empty")
                            {
                                nextPosition = j;
                            }
                        }
                        endPosition = Positions_array[nextPosition].transform.position;
                        PieceToMove = Positions_array[i].GetComponent<CollidingWith>().ObjectInMyPosition;
                        movePiece = true;
                        await MovingObject(PieceToMove, endPosition, 3f);
                    }
                }
            }
        }
    }

    private void CheckPuzzle()
    {
        if (PuzzlePositions.FindIndex(str => str == "PositiveCharger") != -1 && PuzzlePositions.FindIndex(str => str == "Battery") != -1 && PuzzlePositions.FindIndex(str => str == "NegativeCharger") != -1)
        {
            if (PuzzlePositions.FindIndex(str => str == "PositiveCharger") < PuzzlePositions.FindIndex(str => str == "Battery") && PuzzlePositions.FindIndex(str => str == "Battery") < PuzzlePositions.FindIndex(str => str == "NegativeCharger"))
            {
                puzzleEnded = true;
                StartCoroutine(EndPuzzleMovement());
            }

        }
    }
    void Update()
    {
        if(puzzleEnded == false)
        {
            if(InsertExit_Slot.GetComponent<CollidingWith>().InMyPosition == "Battery")
            {
                Debug.Log("We can start");
                StartCoroutine(SmoothLerp(Battery, Position_3.transform.position, 3f));
            }
        }

        PuzzlePositions = new List<string>()
        {   Position_1.GetComponent<CollidingWith>().InMyPosition,
            Position_2.GetComponent<CollidingWith>().InMyPosition,
            Position_3.GetComponent<CollidingWith>().InMyPosition,
            Position_4.GetComponent<CollidingWith>().InMyPosition,
            Position_5.GetComponent<CollidingWith>().InMyPosition  };

        if (movePiece == false && puzzleEnded == false)
        {
            CheckPuzzle();
        }

        RetractedPositions = new List<string>()
        {   Retracted_1.GetComponent<CollidingWith>().InMyPosition,
            Retracted_2.GetComponent<CollidingWith>().InMyPosition };
        if(movePiece==false && puzzleEnded == false)
        {
            if (Input.GetKeyDown("a"))
            {
                if (PuzzlePositions[1] == "Empty" ^ RetractedPositions[0] == "Empty")
                {
                    if (PuzzlePositions[1] == "Empty")
                    {
                        endPosition = Position_2.transform.position;
                        PieceToMove = Retracted_1.GetComponent<CollidingWith>().ObjectInMyPosition;
                    }
                    else
                    {
                        endPosition = Retracted_1.transform.position;
                        PieceToMove = Position_2.GetComponent<CollidingWith>().ObjectInMyPosition;
                    }
                    movePiece = true;
                    StartCoroutine(SmoothLerp(PieceToMove, endPosition, 3f));
                }
            }
            if (Input.GetKeyDown("s"))
            {
                MoveLeftRight();
            }


            if (Input.GetKeyDown("d"))
            {
                if (PuzzlePositions[3] == "Empty" ^ RetractedPositions[1] == "Empty")
                {
                    if (PuzzlePositions[3] == "Empty")
                    {
                        endPosition = Position_4.transform.position;
                        PieceToMove = Retracted_2.GetComponent<CollidingWith>().ObjectInMyPosition;
                    }
                    else
                    {
                        endPosition = Retracted_2.transform.position;
                        PieceToMove = Position_4.GetComponent<CollidingWith>().ObjectInMyPosition;
                    }
                    movePiece = true;
                    StartCoroutine(SmoothLerp(PieceToMove, endPosition, 3f));
                }
            }
        }
    }
}
