using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glass_case : MonoBehaviour
{
    int punchCount = 0;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Animator>(out Animator animComp))
        {
            if (animComp.GetCurrentAnimatorStateInfo(1).IsTag("punch-tag") && punchCount < 3)
            {
                punchCount++;
                Debug.Log(punchCount);
            }
            else if (animComp.GetCurrentAnimatorStateInfo(1).IsTag("punch-tag"))
            {
                this.gameObject.GetComponent<Rigidbody>().useGravity = true;
                if(this.gameObject.transform.position.z < -10)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
                   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
/*        if(this.gameObject.GetComponent<Rigidbody>().useGravity == true && this.gameObject.transform.)
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //this.gameObject.transform.position = new Vector3((float)-1.891, (float)12.125, (float)2.6);
        }*/
    }
}
