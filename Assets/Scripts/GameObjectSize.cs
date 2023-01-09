using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 boxSize = GetComponent<Collider>().bounds.size;
        Debug.Log(boxSize.x);
        Debug.Log(boxSize.y);
        Debug.Log(boxSize.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
