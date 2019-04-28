using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

using CommonsPattern;

public class AutoSalesState : PhaseState
{
    public override PhaseKey Key => PhaseKey.AutoSales;

    public AutoSalesState()
    {
        allowedPreviousStates = new HashSet<PhaseKey>
        {
            PhaseKey.ItemSetup
        };
    }

    public override void OnEnterFrom(PhaseState previousState)
    {
        base.OnEnterFrom(previousState);
        Debug.Log("Entering AutoSales phase");
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
