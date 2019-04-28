using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommonsPattern;

public class ItemSetupManager : SingletonManager<ItemSetupManager>
{
    protected ItemSetupManager () {}

    void Awake () {
        SetInstanceOrSelfDestruct(this);
        Init();
    }
    
    [Tooltip("Item Setup UI root")]
    public GameObject uiRoot;

    void Init()
    {
        
    }

    private void OnEnable()
    {
        uiRoot.SetActive(true);
    }
    
    private void OnDisable()
    {
        uiRoot.SetActive(false);
    }
}
