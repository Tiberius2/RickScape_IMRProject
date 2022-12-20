using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingWith : MonoBehaviour
{
    public string InMyPosition;
    public GameObject ObjectInMyPosition;
    private void Start()
    {
        InMyPosition = "Empty";
    }

    private void OnTriggerEnter(Collider other)
    {
        InMyPosition = other.gameObject.name;
        ObjectInMyPosition = other.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        InMyPosition = "Empty";
    }

}
