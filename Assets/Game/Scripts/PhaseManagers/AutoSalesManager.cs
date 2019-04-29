using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

using CommonsHelper;

public class AutoSalesManager : PhaseManager<AutoSalesManager>
{
    protected AutoSalesManager () {}

    public override void Init()
    {
        SetInstanceOrSelfDestruct(this);
        
        director = this.GetComponentOrFail<PlayableDirector>();
    }

    
    /* Sibling components */
    private PlayableDirector director;
    

    protected override void OnEnableCallback()
    {
        base.OnEnableCallback();
        
        director.Play();
    }

    protected override void OnDisableCallback()
    {
        base.OnDisableCallback();
    }
}
