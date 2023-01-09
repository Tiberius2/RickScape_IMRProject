using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Bullet : MonoBehaviour
{
    public GameObject splash;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject newSplash = Instantiate(splash, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            newSplash.transform.parent = collision.transform;
        }
        else
        {
            Instantiate(splash, this.transform.position, this.transform.rotation);
        }
    }
}
