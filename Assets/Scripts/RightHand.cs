using System.Net;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RightHand : MonoBehaviour
{
    public GameObject phone;
    public GameObject lockedBoxLid;
    public GameObject unlockedBoxLid;

    public GameObject leftButtonHand;
    public bool leftButtonPicked = false;
    public GameObject rightButtonHand;
    public bool rightButtonPicked = false;

    public Map map;
    public LeftHand leftHand;

    public TextMeshPro storageBoxText;
    public float speedValue = 10;

    public bool PhonePicked = false;
    public bool StorageBoxUnlocked = false;

    Animator animator;

    private float punchTarget;
    private float punchCurrent = 0;
    private bool punchableNearby = false;
    private bool pointableNearby = false;

    private readonly int actualPassword = 7953;
    private string inputPassword = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("punch", punchCurrent);
    }

    // Update is called once per frame
    void Update()
    {
        if (PhonePicked || leftButtonPicked || rightButtonPicked)
        {
            SetPoint(false);
        }

        if (leftHand.RadioPicked)
        {
            SetPoint(true);
        }

        AnimateHand();
    }

    internal void SetPunch(float v)
    {
        punchTarget = v;
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
        Debug.Log("other: " + other.tag);

        if (PhonePicked) return;

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

        // pick-up phone
        if (other.CompareTag("phone"))
        {
            other.gameObject.SetActive(false);
            phone.SetActive(true);
            PhonePicked = true;

            SetRotate(true);
        }

        // open box
        else if(unlockedBoxLid.TryGetComponent<Animator>(out Animator animComp) && animComp.CompareTag("box_unlocked_lid") && other.CompareTag("box_unlocked_lid"))
        {
            Debug.Log(animComp.GetBool("open"));
            animComp.SetBool("open", true);
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

        if (other.CompareTag("button_left_out"))
        {
            SetActivity(other.gameObject, leftButtonHand, ref leftButtonPicked);
        }
        else if (other.CompareTag("button_right_out"))
        {
            SetActivity(other.gameObject, rightButtonHand, ref rightButtonPicked);
        }

        if (leftButtonPicked && other.CompareTag("button_left_missing"))
        {
            SetActivity(leftButtonHand, map.leftButton, ref leftButtonPicked);
            map.leftButtonSet = true;
        }

        if (rightButtonPicked && other.CompareTag("button_right_missing"))
        {
            SetActivity(rightButtonHand, map.rightButton, ref rightButtonPicked);
            map.rightButtonSet = true;
        }

        if(map.rightButtonSet && map.leftButtonSet && !map.gameStarted && other.CompareTag("button_choose"))
        {
            map.StartGame();
        }

        if(map.gameStarted && other.CompareTag("button_choose"))
        {
            map.Choose();
        }

        if(map.gameStarted && other.CompareTag("button_set"))
        {
            map.Set();
        }

        if(map.gameStarted && other.CompareTag("button_cancel"))
        {
            map.Cancel();
        }
    }

    private void SetActivity(GameObject toFalse, GameObject toTrue, ref bool activityFlag)
    {
        toFalse.SetActive(false);
        toTrue.SetActive(true);
        activityFlag = !activityFlag;
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
                //storageBoxText.text = string.Empty;
            }
        }
    }
}
