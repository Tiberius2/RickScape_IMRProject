using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public Transform portalGunPosition;
    public Vector3 inParentPosition;
    private int count = 0;

    [ContextMenu("Test Move")]
    public void Move()
    {

        LeanTween.move(gameObject, portalGunPosition.position, 1f).setEaseInOutQuad().setOnComplete(() =>
        {
            count++;
            Debug.Log("Count = " + count);
            gameObject.transform.SetParent(portalGunPosition);
            gameObject.transform.localPosition=inParentPosition;

        });
    }

}
