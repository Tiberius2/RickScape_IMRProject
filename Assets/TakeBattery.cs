using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBattery : MonoBehaviour
{
    public bool GotBattery = false;

    private void OnTriggerEnter(Collider other)
    {
        if (GotBattery == false)
        {
            if (other.gameObject.CompareTag("Battery"))
            {
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                other.gameObject.transform.position = this.transform.position;
                other.gameObject.transform.rotation = this.transform.rotation;
                GotBattery = true;
            }
        }

    }
}
