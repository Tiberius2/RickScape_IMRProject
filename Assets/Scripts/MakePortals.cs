using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePortals : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public ControllerScript controllerScript;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.other.CompareTag("right_hand") || collision.other.CompareTag("left_hand"))
        {
            controllerScript.Room2Done = true;
        }
    }
}
