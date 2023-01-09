using UnityEngine;

public class FlyColor : MonoBehaviour
{
    public ColorEnum.MyColor myColor;
    public bool correctMatch = false;

    public Material MyMaterial;
    public Material WhiteMaterial;

    public SenzorCorrectColor senzorCorrectColor;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Renderer>().material = MyMaterial;
        senzorCorrectColor = other.GetComponent<SenzorCorrectColor>();
        if (myColor == senzorCorrectColor.myCorrectColor)
        {
            correctMatch = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Renderer>().material = WhiteMaterial;
        correctMatch = false;
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
