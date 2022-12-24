using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LeftHand : MonoBehaviour
{
    Animator animator;
    private float grabTarget;
    private float grabCurrent = 0;
    public float speedValue = 10;

    public GameObject flashlight;
    public GameObject flashlightCase;
    internal void SetGrab(float v)
    {
        grabTarget = v;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("grab", grabCurrent);

    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    void AnimateHand()
    {  
        if (grabCurrent != grabTarget)
        {
            grabCurrent = Mathf.MoveTowards(grabCurrent, grabTarget, Time.deltaTime * speedValue);
            animator.SetFloat("grab", grabCurrent);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (this.TryGetComponent<Animator>(out Animator animComp))
        {
            if (animComp.GetCurrentAnimatorStateInfo(1).IsTag("grab-tag") &&
                other.gameObject.CompareTag("flashlight") &&
                flashlightCase.GetComponent<Rigidbody>().useGravity
                )
            {
                    other.gameObject.SetActive(false);
                    flashlight.SetActive(true);

                    animComp.SetBool("grabbed", true);            
            }
        }
    }
}
