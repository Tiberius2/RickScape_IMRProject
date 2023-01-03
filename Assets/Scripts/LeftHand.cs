using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LeftHand : MonoBehaviour
{
    Animator animator;
    //private float grabTarget;
    //private float grabCurrent = 0;
    //public float speedValue = 10;

    public GameObject flashlight;
    public Radio radio;
    public GlassCase flashlightCase;
    public RightHand rightHand;

    public bool FlashlightPicked = false;
    public bool RadioPicked = false;

    /*    internal void SetGrab(float v)
        {
            grabTarget = v;
        }*/

    internal void SetGrab(bool state)
    {
        animator.SetBool("grabb", state);
    }

    internal void SetGrabRadio(bool state)
    {
        animator.SetBool("grab_radio", state);
    }

/*    internal void StopGrab(float v)
    {
        animator.SetFloat("grab", v);
        FlashlightPicked = false;
        Debug.Log("FlashlightDropped");
    }*/

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //animator.SetFloat("grab", grabCurrent);

    }

    // Update is called once per frame
    void Update()
    {
        //AnimateHand();
    }

    void AnimateHand()
    {  
/*        if (grabCurrent != grabTarget)
        {
            grabCurrent = Mathf.MoveTowards(grabCurrent, grabTarget, Time.deltaTime * speedValue);
            animator.SetFloat("grab", grabCurrent);
            FlashlightPicked = true;
            Debug.Log("FlashlightPicked");
        }*/
    }



    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("flashlight") && !RadioPicked && flashlightCase.caseFell)
        {
            other.gameObject.SetActive(false);
            flashlight.SetActive(true);
            FlashlightPicked = true;

            SetGrab(true);
        }
        else if (other.CompareTag("radio") && !FlashlightPicked && rightHand.StorageBoxUnlocked)
        {
            other.gameObject.SetActive(false);
            radio.gameObject.SetActive(true);
            RadioPicked = true;

            SetGrabRadio(true);
        }

/*        if (this.TryGetComponent<Animator>(out Animator animComp))
        {
            if (animComp.GetCurrentAnimatorStateInfo(1).IsTag("grab-tag") &&
                other.gameObject.CompareTag("flashlight") &&
                flashlightCase.caseFell == true
                )
            {
                    other.gameObject.SetActive(false);
                    flashlight.SetActive(true);

                    animComp.SetBool("grabbed", true);            
            }
        }*/
    }
}
