using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Map : MonoBehaviour
{
    class GameInterface
    {
        public int Index { get; set; }
        public string Planet { get; set; }
    }

    Dictionary<int, string> planets = new() {
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

    public bool gameStarted = false;
    public int gameIndex = 0;
    public int gameStage = 0;

    private string screenText = "Choose places to nuke: ";

    private List<GameInterface> gameInterface = new List<GameInterface>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(leftButtonSet && rightButtonSet && !gameStarted)
        {
            leftCollider.SetActive(false);
            rightCollider.SetActive(false);

            mapScreen.screenText.text = "Press green to start, then use green to choose, orange to set and red to restart";
            mapScreen.screenText.fontSize = 24;
        }

        if(gameStarted && gameStage == 4)
        {

        }
    }

    public void StartGame()
    {
        StartCoroutine(WaitCoroutine());

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
        gameInterface.Add(new GameInterface() { Index = gameStage, Planet = planets[gameIndex] });
        screenText += $"\r\n {gameStage}. {planets[gameIndex]}";

        gameStage++;

        mapScreen.screenText.text = screenText + $"\r\n {gameStage}. {planets[gameIndex]}";
    }

    public void Cancel()
    {
        gameStage = 0;
        gameStarted = false;

        screenText = "Choose places to nuke: ";
        gameInterface.Clear();
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(3);
    }
}
