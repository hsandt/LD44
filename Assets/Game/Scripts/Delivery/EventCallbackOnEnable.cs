using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// Activate this script's game object in a Timeline to simulate Timeline signals (Unity 2019)
public class EventCallbackOnEnable : MonoBehaviour
{
    [SerializeField, Tooltip("Method to call when this game object is activated")]
    private UnityEvent eventCallback = new UnityEvent();
    
    private void Awake()
    {
        // to avoid calling the event on an initial OnEnable if the game object starts
        // Active in the scene, deactivate it now (don't disable the script, as the timeline
        // really uses game object activation)
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        eventCallback.Invoke();
    }
}
