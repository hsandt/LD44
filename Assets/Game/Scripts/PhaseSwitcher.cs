using CommonsHelper;
using CommonsPattern;
using UnityEngine;

public class PhaseSwitcher : SingletonManager<PhaseSwitcher>
{
    protected PhaseSwitcher () {}
    
    void Awake () {
    	SetInstanceOrSelfDestruct(this);
        Init();
    }
    
    private FSMMachine<PhaseKey, PhaseState> m_FSM;
        
    [Tooltip("Item Setup Manager root")]
    public GameObject itemSetupManager;

    [Tooltip("Auto-Sales Setup Manager root")]
    public GameObject autoSalesManager;

    [Tooltip("Auto-Sales Result Setup Manager root")]
    public GameObject autoSalesResultManager;

    private GameObject GetManagerRoot(PhaseKey key)
    {
        switch (key)
        {
            case PhaseKey.ItemSetup:
                return itemSetupManager;
            case PhaseKey.AutoSales:
                return autoSalesManager;
            case PhaseKey.AutoSalesResult:
                return autoSalesResultManager;
            default:
                return null;
        }
    }

    private GameObject currentPhaseManager = null;
        
    void Init () {
        m_FSM = new FSMMachine<PhaseKey, PhaseState>();
        m_FSM.AddState(new ItemSetupState());
        m_FSM.AddState(new AutoSalesState());;
        m_FSM.AddState(new AutoSalesResultState());;
        m_FSM.SetDefaultStateByKey(PhaseKey.ItemSetup);
    }

    void Start ()
    {
        m_FSM.Setup();

        // mirror FSM by activating initial phase manager, but only this one
        DeactivateAllPhaseManagers();
        SwitchToPhaseManager(PhaseKey.ItemSetup);
    }

    void FixedUpdate () {
        m_FSM.UpdateMachine();
    }
    
    void Clear () {
        m_FSM.Clear();
    }
    
    private void DeactivateAllPhaseManagers()
    {
        foreach (PhaseKey key in EnumUtil.GetValues<PhaseKey>())
        {
            GameObject phaseManagerGO = GetManagerRoot(key);
            if (phaseManagerGO != null)
            {
                phaseManagerGO.SetActive(false);
            }
        }
    }

    [EnumAction(typeof(PhaseKey))]
    public void GoToPhase(int phaseIndex)
    {
        PhaseKey key = (PhaseKey) phaseIndex;
        
        // custom FSM
        m_FSM.SetNextStateByKey(key);
        
        // Unity MonoBehaviour on root game object
        SwitchToPhaseManager(key);
    }

    private void SwitchToPhaseManager(PhaseKey key)
    {
        if (currentPhaseManager != null)
        {
            currentPhaseManager.SetActive(false);
        }

        currentPhaseManager = GetManagerRoot(key);
        if (currentPhaseManager != null)
        {
            currentPhaseManager.SetActive(true);
        }
    }
}
