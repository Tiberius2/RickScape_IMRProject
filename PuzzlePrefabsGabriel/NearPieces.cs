using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearPieces : MonoBehaviour
{
    public bool up_open;
    public bool down_open;
    public bool left_open;
    public bool right_open;
    public float checkDistance = 5f;
    public LayerMask anotherPiece;
    void Update()
    {
        up_open = !Physics.Raycast(this.transform.position, this.transform.forward, checkDistance, anotherPiece);
        down_open = !Physics.Raycast(this.transform.position, -this.transform.forward, checkDistance, anotherPiece);
        right_open = !Physics.Raycast(this.transform.position, this.transform.up, checkDistance, anotherPiece);
        left_open = !Physics.Raycast(this.transform.position, -this.transform.up, checkDistance, anotherPiece);


    }
}
