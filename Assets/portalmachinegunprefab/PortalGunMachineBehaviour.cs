using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGunMachineBehaviour : MonoBehaviour
{
    #region Singleton
    public static PortalGunMachineBehaviour instance;
   /* private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }*/
    #endregion
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();

    }
    [ContextMenu("Test Open")]
    public void Open()
    {
        _anim.SetBool("Open", true);
    }
}
