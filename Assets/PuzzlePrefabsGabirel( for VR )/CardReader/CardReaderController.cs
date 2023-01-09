using UnityEngine;

public class CardReaderController : MonoBehaviour
{
    public GameObject bec1;
    public GameObject bec2;
    public GameObject bec3;
    public GameObject bec4;
    public GameObject bec5;
    public GameObject CardAcceptedScreen;
    public bool CardAcceptedBool = false;
    public bool CardAcceptedConfirmed = false;
    public float currentTime = 0f;
    public float endTime = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Card")
        {
            currentTime = 0;
            CardAcceptedConfirmed = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Card")
        {
            currentTime += 1 * Time.deltaTime;
            if (currentTime > endTime)
            {
                currentTime = 0f;
            }

            if (currentTime >= 3f && currentTime < 4f)
            {
                bec1.SetActive(true);
                bec2.SetActive(true);
                bec3.SetActive(true);
                bec4.SetActive(false);
                bec5.SetActive(true);
                CardAcceptedScreen.SetActive(true);
                CardAcceptedBool = true;
            }
            else
            {
                CardAcceptedScreen.SetActive(false);
                CardAcceptedBool = false;
                bec4.SetActive(true);

                if (currentTime >= 4f && currentTime < 5f)
                {
                    bec1.SetActive(true);
                    bec2.SetActive(true);
                    bec3.SetActive(true);
                    bec5.SetActive(false);
                }
                if (currentTime >= 2f && currentTime < 3f)
                {
                    bec1.SetActive(true);
                    bec2.SetActive(true);
                    bec3.SetActive(false);
                    bec5.SetActive(true);
                }
                else if (currentTime >= 1f && currentTime < 2f)
                {
                    bec1.SetActive(true);
                    bec2.SetActive(false);
                    bec3.SetActive(true);
                    bec5.SetActive(true);
                }
                else if (currentTime >= 0f && currentTime < 1f)
                {
                    bec1.SetActive(false);
                    bec2.SetActive(true);
                    bec3.SetActive(true);
                    bec5.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Card")
        {
            if (CardAcceptedBool == true)
            {
                CardAcceptedConfirmed = true;
                Debug.Log(CardAcceptedConfirmed);
            }
        }
    }

}
