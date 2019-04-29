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

    private bool initialized = false;
    
    void Awake () {
        TryInit();
    }

    private void TryInit()
    {
        if (!initialized)
        {
            // if the script does not call OnEnable before being disabled
            // by the PhaseSwitcher, it does not call OnDisable either,
            // so call it manually to make sure you hide UI on Start
            OnDisableCallback();
            Init();
            initialized = true;
        }
    }

    // Actually called via SendMessage, so public is not really used
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
