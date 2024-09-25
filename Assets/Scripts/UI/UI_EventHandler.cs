// ----- C#
using System;

// ----- Unit
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler,IDropHandler,IPointerUpHandler
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnDropHandler = null;
    public Action<PointerEventData> OnPointerUpHandler = null;

    // --------------------------------------------------
    // Functions - Event
    // --------------------------------------------------
    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnClickHandler !=null)
            OnClickHandler.Invoke(eventData);
    }   
    public void OnDrag(PointerEventData eventData)
    {
        if(OnDragHandler !=null)
            OnDragHandler.Invoke(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(OnDropHandler !=null)
            OnDropHandler.Invoke(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(OnPointerUpHandler !=null)
            OnPointerUpHandler.Invoke(eventData);
    }
}