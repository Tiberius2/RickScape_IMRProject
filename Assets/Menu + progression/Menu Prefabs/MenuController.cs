using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public Transform head;
    public float distance = 2f;
    public GameObject MainMenu;
    public GameObject CreditsPage;
    public InputActionProperty toggleButton;
    public string Levels_1_and_2;

    void Update()
    {
        if(name == "inGameMenu")
        {
            if (toggleButton.action.WasPerformedThisFrame())
            {

                MainMenu.SetActive(!MainMenu.activeSelf);

                MainMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * distance;
            }

            MainMenu.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            MainMenu.transform.forward *= -1;
        }
    }

    // Main Menu functions
    public void StartGame()
    {
        SceneManager.LoadScene(Levels_1_and_2);
    }
    public void Credits()
    {
        MainMenu.SetActive(false);
        CreditsPage.SetActive(true);
    }

    public void CreditsBack()
    {
        CreditsPage.SetActive(false);
        MainMenu.SetActive(true);
    }

    // inGame Menu functions

    // Common functions
    public void Quit()
    {
        Application.Quit();
    }


}
