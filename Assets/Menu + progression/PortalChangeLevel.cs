using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalChangeLevel : MonoBehaviour
{
    public string Level3;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            SceneManager.LoadScene(Level3);
        }
    }
}
