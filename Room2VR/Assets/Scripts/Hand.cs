using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    Animator animator;
    private float punchTarget;
    private float punchCurrent = 0;
    public float speedValue = 10;
    public GameObject phone;
    public GameObject lockedBoxLid;
    public bool PhonePicked = false;
    public bool StorageBoxUnlocked = false;

    private bool punchableNearby = false;
    private bool pointableNearby = false;

    public LeftHand leftHand;

    public TextMeshPro storageBoxText;

    private int actualPassword = 7953;
    private string inputPassword = string.Empty;

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

        if (leftHand.RadioPicked)
        {
            SetPoint(true);
        }

        AnimateHand();
    }

    void AnimateHand()
    {
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

            if (pointableNearby && !StorageBoxUnlocked)
            {
                if (int.TryParse(other.tag, out int number))
                {
                    storageBoxText.color = Color.white;
                    inputPassword += number;
                    storageBoxText.text = inputPassword;

                    CheckPassword();
                }
            }

            if (leftHand.RadioPicked)
            {
                if (other.CompareTag("radio"))
                {
                    leftHand.radio.PressButton();
                }
            }
        }
    }

    private void CheckPassword()
    {
        if (inputPassword.Length == 4)
        {
            if (int.TryParse(inputPassword, out int numpadPassword) && numpadPassword == actualPassword)
            {
                storageBoxText.color = Color.green;
                StorageBoxUnlocked = true;
                SetPoint(false);
                if (lockedBoxLid.TryGetComponent<Animator>(out Animator animComp) && animComp.CompareTag("box_locked_lid"))
                {
                    animComp.SetBool("should_open", true);
                }
            }
            else
            {
                storageBoxText.color = Color.red;
                inputPassword = string.Empty;
            }
        }
    }
}
