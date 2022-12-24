using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    Animator animator;
    private float punchTarget;
    private float punchCurrent = 0;
    public float speedValue = 10;
    public GameObject phone;
    public bool PhonePicked = false;

    private bool punchableNearby = false;
    private bool pointableNearby = false;

    private int numpadPassword = 7531;
    private string inputPassword = "";

    internal void SetPunch(float v)
    {
        punchTarget = v;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("punch", punchCurrent);
    }

    // Update is called once per frame
    void Update()
    {
        if (PhonePicked)
        {
            SetPoint(false);
        }

        AnimateHand();
    }

    void AnimateHand()
    {
        //Debug.Log("current:"+ punchCurrent.ToString() + "; target:" + punchTarget.ToString());

        if (punchCurrent != punchTarget)
        {
            punchCurrent = Mathf.MoveTowards(punchCurrent, punchTarget, Time.deltaTime * speedValue);
            animator.SetFloat("punch", punchCurrent);
        }
    }

    public void SetRotate(bool state)
    {
        animator.SetBool("rotate", state);
    }

    public void SetPoint(bool state)
    {
        animator.SetBool("point", state);
    }
                                              
    public void Action(float action)
    {
        if (punchableNearby)
        {
            SetPunch(action);
            SetPoint(false);
        }
        else if (pointableNearby)
        {
            SetPoint(true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (!PhonePicked)
        {
            if (other.CompareTag("punch_zone"))
            {
                punchableNearby = true;
                pointableNearby = false;
            }
            else if (other.CompareTag("point_zone"))
            {
                punchableNearby = false;
                pointableNearby = true;
            }

            if (other.CompareTag("phone"))
            {

                other.gameObject.SetActive(false);
                phone.SetActive(true);
                PhonePicked = true;

                SetRotate(true);
            }

            if (pointableNearby)
            {
                if (int.TryParse(other.tag, out int number))
                {
                    inputPassword = inputPassword + other.tag;
                    if(inputPassword.Length == 4)
                    {
                        CheckPassword(inputPassword);
                    }
                }
            }
        }
    }

    private void CheckPassword(string inputPassword)
    {
        int.TryParse(inputPassword, out int input);
        if(input != 0 && input == numpadPassword)
        {

        }
    }
}
