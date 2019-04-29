using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommonsPattern;

public abstract class PhaseManager<T> : SingletonManager<T> where T : PhaseManager<T>
{
    /* TEMPLATE
     
    protected T () {}

    public void Init()
    {
        SetInstanceOrSelfDestruct(this);
    }
    
    */

    void Awake () {
        Init();
    }

    public abstract void Init();
    
    [Tooltip("Item Setup UI root")]
    public GameObject uiRoot;

    private void OnEnable()
    {
        OnEnableCallback();
    }

    protected virtual void OnEnableCallback()
    {
        uiRoot.SetActive(true);
    }
    
    private void OnDisable()
    {
        OnDisableCallback();
    }

    protected virtual void OnDisableCallback()
    {
        // when stopping game in Editor, OnDisable is called but other objects may have been destroyed
        if (uiRoot != null)
        {
            uiRoot.SetActive(false);
        }
    }
}
