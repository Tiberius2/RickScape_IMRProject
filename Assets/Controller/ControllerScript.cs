using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerScript : MonoBehaviour
{
    // Room 1
    public bool FlyPuzzle;
    public bool SimonSays;
    public bool WirePuzzle;

    // Room 2
    public bool Room2Done;

    // Room 3
    public bool GOWPuzzle;
    public bool RoundPuzzle;
    public bool BatteriesPlaced;
    public bool EightPuzzle;
    public bool CardPuzzle;
    public bool Ending1;
    public bool Ending2;

    public GameObject box;
    public GameObject wires;
    public GameObject robot;
    public GameObject room1;
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject player;
    public GameObject suit;
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FlyPuzzle)
        {
            box.gameObject.SetActive(true);
            wires.gameObject.SetActive(true);
            robot.gameObject.SetActive(true);
        }
        if(SimonSays)
        {
            box.gameObject.SetActive(false);
        }
        if(WirePuzzle)
        {
            room1.gameObject.SetActive(false);
        }
        if(Room2Done)
        {
            Room2Done = false;
            player.gameObject.transform.position = new Vector3(46.02f, 13.456f, -23.53f);
        }
        if(EightPuzzle)
        {
            door1.gameObject.SetActive(false);
        }
        if(GOWPuzzle && RoundPuzzle)
        {
            door2.gameObject.SetActive(false);
        }
        if (CardPuzzle)
        {
            door3.gameObject.SetActive(false);
        }
        if (Ending1)
        {
            suit.gameObject.SetActive(false);
            text.gameObject.SetActive(true);
        }
        if (Ending2)
        {
            SceneManager.LoadScene("Ending2");
        }
    }
}
