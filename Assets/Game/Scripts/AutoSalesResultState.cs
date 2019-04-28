using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommonsPattern;

public class AutoSalesResultState : PhaseState
{
    public override PhaseKey Key => PhaseKey.AutoSalesResult;

    public AutoSalesResultState()
    {
        allowedPreviousStates = new HashSet<PhaseKey>
        {
            PhaseKey.AutoSales
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
