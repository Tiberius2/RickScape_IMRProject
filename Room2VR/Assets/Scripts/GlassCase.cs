using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCase : MonoBehaviour
{
    int punchCount = 0;
    public bool caseFell = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Animator>(out Animator animComp))
        {
            if (animComp.GetCurrentAnimatorStateInfo(0).IsTag("punch-tag") && punchCount < 3)
            {
                punchCount++;
                Debug.Log(punchCount);
            }
            else if (animComp.GetCurrentAnimatorStateInfo(0).IsTag("punch-tag"))
            {
                if (this.TryGetComponent<Animator>(out Animator caseAnim))
                {
                    caseAnim.SetBool("should_fall", true);
                    caseFell = true;
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
    }
}
