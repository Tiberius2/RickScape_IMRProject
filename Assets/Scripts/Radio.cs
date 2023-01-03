using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Radio : MonoBehaviour
{
    private readonly Queue<string> frequencies = new ();
    public TextMeshPro radioText;

    public void PressButton()
    {
        string toSshow = frequencies.Dequeue();
        frequencies.Enqueue(toSshow);

        radioText.text = toSshow;
    }

    // Start is called before the first frame update
    void Start()
    {
        frequencies.Enqueue("1.3266 THz");
        frequencies.Enqueue("0.3266 THz");
        frequencies.Enqueue("2.3787 THz");
        frequencies.Enqueue("1.6315 THz");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
