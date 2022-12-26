using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public GameObject lockedText;
    public GameObject unlockedText;
    public GameObject slideshow;

    public void Unlock()
    {
        lockedText.SetActive(false);
        unlockedText.SetActive(true);
        slideshow.SetActive(true);
        Debug.Log("Phone unlocked");
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
