using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlightraycast : MonoBehaviour
{
    public const int MAX_DIGITS = 5;

    public float range = 500;

    [Header("Object References")]
    public GameObject source;

    [Header("Object References")]
    public Phone phone;


    private GameObject lastHit;
    private int count = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.forward;

        Ray ray = new Ray(source.transform.position, source.transform.TransformDirection(direction * range));
        Debug.DrawRay(source.transform.position, source.transform.TransformDirection(direction * range));

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if (hit.collider.CompareTag("HasHiddenText"))
            {
                lastHit = hit.collider.gameObject.transform.GetChild(0).gameObject;
                if (!lastHit.activeSelf)
                {
                    count++;
                    lastHit.SetActive(true);
                    if (count >= MAX_DIGITS)
                    {
                        phone.Unlock();
                    }
                }
            }
        }
    }
}
