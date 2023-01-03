using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Map : MonoBehaviour
{
    private readonly Dictionary<int, string> planets = new() {
        { 1, "Nothing" },
        { 2, "Jupiter" },
        { 3, "Gazorpazorp" },
        { 4, "CronenbergWorld" },
        { 5, "Saturn" },
        { 6, "Spaceship" },
        { 7, "Earth" }
    };

    public MapScreen mapScreen;

    public GameObject leftButton;
    public bool leftButtonSet = false;
    public GameObject rightButton;
    public bool rightButtonSet = false;

    // colliders used to add buttons to wall (must be deactivated after buttons are set)
    public GameObject leftCollider;
    public GameObject rightCollider;
    public GameObject closedBox;

    public bool gameStarted = false;
    public bool gameFinished = false;
    public int gameIndex = 0;
    public int gameStage = 0;

    private string screenText = "Choose places to nuke: ";

    private List<string> records = new();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(leftButtonSet && rightButtonSet && !gameStarted && !gameFinished)
        {
            leftCollider.SetActive(false);
            rightCollider.SetActive(false);

            records.Clear();
            screenText = "Choose places to nuke: ";

            mapScreen.screenText.text = "Press green to start, then use green to choose, orange to set and red to restart";
            mapScreen.screenText.fontSize = 24;
            mapScreen.screenText.color = Color.white;
        }

        if(gameStarted && gameStage == 4)
        {
            CheckWin();
            gameStarted = false;
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        gameStage = 1;
        mapScreen.screenText.text = $"Choose places to nuke:\r\n {gameStage}. {planets.First().Key}";
        mapScreen.screenText.fontSize = 24;
    }

    public void Choose()
    {
        if (gameIndex >= 7) gameIndex = 0;

        gameIndex++;
        mapScreen.screenText.text = screenText + $"\r\n {gameStage}. {planets[gameIndex]}";
    }

    public void Set()
    {
        //records.Add(new Record() { Index = gameStage, Planet = planets[gameIndex] });
        records.Add(planets[gameIndex]);
        screenText += $"\r\n {gameStage}. {planets[gameIndex]}";

        gameStage++;

        mapScreen.screenText.text = screenText + $"\r\n {gameStage}. {planets[gameIndex]}";
    }

    public void Cancel()
    {
        gameStage = 1;
        gameStarted = false;

        screenText = $"Choose places to nuke:\r\n {gameStage}. {planets.First().Key}";
        mapScreen.screenText.fontSize = 24;

        records.Clear();
    }

    private bool CheckWin()
    {
        if (records.Count == 3)
        {
            records.RemoveAll(x => x.Equals("Nothing"));

            if (records.Count == 1 && records.First().Equals("Spaceship"))
            {
                mapScreen.screenText.text = "Verdict: Not an idiot!";
                mapScreen.screenText.fontSize = 30;
                mapScreen.screenText.color = Color.green;

                if(closedBox.TryGetComponent<Animator>(out Animator animComp) && animComp.CompareTag("wall_safe_door"))
                {
                    animComp.SetBool("open_wall_door", true);
                }

                gameFinished = true;
                return true;
            }
        }

        mapScreen.screenText.text = "YOU \r\n LOSE";
        mapScreen.screenText.fontSize = 45;
        mapScreen.screenText.color = Color.red;

        gameFinished = true;
        return false;
    }
}
