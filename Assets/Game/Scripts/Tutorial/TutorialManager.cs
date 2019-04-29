using System.Collections;
using System.Collections.Generic;
using CommonsPattern;
using UnityEngine;

public class TutorialManager : SingletonManager<TutorialManager>
{
    protected TutorialManager () {}
    
    void Awake () {
    	SetInstanceOrSelfDestruct(this);
    }
    
    [Tooltip("Tutorial panel main script")]
    public Tutorial tutorial;
    
    void Start()
    {
        tutorial.gameObject.SetActive(false);    
    }

    private void OnEnable()
    {
        ItemSetupManager.ExposeItemEvent += tutorial.OnExposedItem;
        ItemSetupManager.PullBackItemEvent += tutorial.OnPulledBackItem;
    }

    private void OnDisable()
    {
        ItemSetupManager.ExposeItemEvent -= tutorial.OnExposedItem;
        ItemSetupManager.PullBackItemEvent -= tutorial.OnPulledBackItem;
    }


    // Update is called once per frame
    public void ShowTutorial(TutorialKey key)
    {
        tutorial.gameObject.SetActive(true);
        tutorial.ShowTutorialText(key);
    }

    public void HideTutorial()
    {
        tutorial.gameObject.SetActive(false);    
    }
}
