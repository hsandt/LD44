// A trick using OnRectTransformDimensionsChange to detect when
// a rect transform is changing.
// Usage:
// 1. Add this UI Behaviour to the game object having the RectTransform you want to monitor
// 2. Plug to the event handler to resize child elements, etc., accordingly.

// See discussions on:
// https://forum.unity.com/threads/window-resize-event.40253/
// https://forum.unity.com/threads/grid-layout-group-and-dynamic-cell-size.271909/

using UnityEngine.EventSystems;

public class ResizeEventHandler : UIBehaviour
{
    public delegate void OnUIResize();
    public OnUIResize UIResizeEvent;
    
    protected override void OnRectTransformDimensionsChange ()
    {
        UIResizeEvent?.Invoke();
    }
}