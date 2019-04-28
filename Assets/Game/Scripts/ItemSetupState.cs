using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommonsPattern;

public class ItemSetupState : PhaseState
{
    public override PhaseKey Key {
        get { return PhaseKey.ItemSetup; }
    }
    
    public ItemSetupState()
    {
        allowedPreviousStates = new HashSet<PhaseKey>
        {
            PhaseKey.None
        };
    }

    public override void OnEnterFrom(PhaseState previousState)
    {
        base.OnEnterFrom(previousState);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void OnExitTo(PhaseState nextState)
    {
        base.OnExitTo(nextState);
    }
}
