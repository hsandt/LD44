using CommonsHelper;
using CommonsPattern;
using UnityEngine;

// SEO: before any Phase Manager, to avoid having an extra OnEnable call on each of them
// before they are all deactivated during initialization
public class PhaseSwitcher : SingletonManager<PhaseSwitcher>
{
    protected PhaseSwitcher () {}
    
    void Awake () {
    	SetInstanceOrSelfDestruct(this);
        Init();
    }
    
    [Tooltip("Delivery Manager root")]
    public SessionManager sessionManager;

    [Tooltip("Delivery Manager root")]
    public GameObject deliveryManager;

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
            case PhaseKey.Delivery:
                return deliveryManager;
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
        DeactivateAllPhaseManagers();
    }

    public void OnStartGameSession ()
    {
        SwitchToPhaseManager(PhaseKey.Delivery);
    }

    void FixedUpdate () {
    }
    
    void Clear () {
    }
    
    private void DeactivateAllPhaseManagers()
    {
        foreach (PhaseKey key in EnumUtil.GetValues<PhaseKey>())
        {
            GameObject phaseManagerGO = GetManagerRoot(key);
            if (phaseManagerGO != null)
            {
                // Since we are disable phase managers early to avoid OnEnable,
                // at least we call init to register the singleton instances
                // and load resources.
                // Unfortunately, PhaseManager<T> cannot be obtained as a generic,
                // so we must use that ugly reflection call.
                // The alternative is to force SEO of all phase managers *before*
                // the Phase Switcher, and ask them to deactivate themselves on Awake
                // just before the call to OnEnable.
                // Use TryInit to avoid initializing twice.
                phaseManagerGO.SendMessage("TryInit");
                phaseManagerGO.SetActive(false);
            }
        }
        Debug.Log("[PhaseSwitcher] Deactivated all phase managers");
    }

    [EnumAction(typeof(PhaseKey))]
    public void GoToPhase(int phaseIndex)
    {
        PhaseKey key = (PhaseKey) phaseIndex;
        SwitchToPhaseManager(key);
    }

    private void SwitchToPhaseManager(PhaseKey key)
    {
        if (currentPhaseManager != null)
        {
            currentPhaseManager.SetActive(false);
            Debug.LogFormat(currentPhaseManager, "[PhaseSwitcher] Deactivated previous phase managers {0}", currentPhaseManager);
        }

        currentPhaseManager = GetManagerRoot(key);
        if (currentPhaseManager != null)
        {
            currentPhaseManager.SetActive(true);
            Debug.LogFormat(currentPhaseManager, "[PhaseSwitcher] Activated new phase managers {0}", currentPhaseManager);
        }
    }
}
