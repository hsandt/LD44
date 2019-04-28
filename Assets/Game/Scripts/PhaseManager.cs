using System.Collections;
using System.Collections.Generic;
using CommonsPattern;
using UnityEngine;

public class PhaseManager : SingletonManager<PhaseManager>
{
    private FSMMachine<PhaseKey, PhaseState> m_FSM;
        
    void Awake () {
        m_FSM = new FSMMachine<PhaseKey, PhaseState>();
        m_FSM.AddState(new ItemSetupState());
        m_FSM.AddState(new AutoSalesState());;
        m_FSM.AddState(new AutoSalesResultState());;
        m_FSM.SetDefaultStateByKey(PhaseKey.ItemSetup);
    }

    void Start () {
        m_FSM.Setup();
    }

    void FixedUpdate () {
        m_FSM.UpdateMachine();
    }
    
    void Clear () {
        m_FSM.Clear();
    }

    [EnumAction(typeof(PhaseKey))]
    public void GoToPhase(int phaseIndex)
    {
        m_FSM.SetNextStateByKey((PhaseKey)phaseIndex);
    }    
}
