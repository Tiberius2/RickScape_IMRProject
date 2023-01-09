using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGunMachineBehaviour : MonoBehaviour
{
    public bool allPiecesCollected = true;

    #region Singleton
    public static PortalGunMachineBehaviour instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();

    }
    void Update()
    {
        if (allPiecesCollected)
        {
            Debug.Log("working");
            Open();
        }
    }
    [ContextMenu("Test Open")]
    public void Open()
    {
        _anim.SetBool("Open", true);
    }
}
