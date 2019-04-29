using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// Activate this script's game object in a Timeline to simulate Timeline signals (Unity 2019)
/// to show a tutorial by key
public class ShowTutorialOnEnable : MonoBehaviour
{
    [SerializeField, Tooltip("Key of tutorial to show")]
    private TutorialKey tutorialKey = TutorialKey.Welcome;
    
    private void Awake()
    {
        // to avoid calling the event on an initial OnEnable if the game object starts
        // Active in the scene, deactivate it now (don't disable the script, as the timeline
        // really uses game object activation)
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        TutorialManager.Instance.ShowTutorial(tutorialKey);
    }
}
