using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Material myMaterial;

    private void OnTriggerEnter(Collider other)
    {
        myMaterial.color = Color.green;
        PortalGunMachineBehaviour.instance.Open();       
    }
    private void OnTriggerExit(Collider other)
    {
            myMaterial.color = Color.red;      
    }

}
