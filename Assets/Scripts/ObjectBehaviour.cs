using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public Transform portalGunPosition;
    public Vector3 inParentPosition;
    [ContextMenu("Test Move")]
    public void Move()
    {
        
        LeanTween.move(gameObject, portalGunPosition.position, 1f).setEaseInOutQuad().setOnComplete(() =>
        {
            gameObject.transform.SetParent(portalGunPosition);
            gameObject.transform.localPosition=inParentPosition;

        });
    }

}
