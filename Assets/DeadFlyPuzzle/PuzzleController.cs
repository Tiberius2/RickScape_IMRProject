using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public FlyColor musca_1;
    public FlyColor musca_2;
    public FlyColor musca_3;
    public FlyColor musca_4;
    public FlyColor musca_5;

    // Update is called once per frame
    void Update()
    {
        if(musca_1.correctMatch && musca_2.correctMatch && musca_3.correctMatch && musca_4.correctMatch && musca_5.correctMatch)
        {
            Debug.Log("Solved Puzzle");
        }
    }
}
