using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
#region Private Variables
    private RectTransform rectTransform;
    private Vector3 startPosition;
    private Vector2 startMousePosition;
    private Vector2 offset;
    private bool isDragging;
    private List<string> directions = new List<string> { "Up", "Down", "Left", "Right" };
    private SoundManager soundManager;
    private GameObject locker;
#endregion

#region Private Methods
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
        soundManager = GameManager.instance.soundManager;
        locker = transform.parent.gameObject;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < directions.Count; i++) {
            if (collision.transform.name != directions[i]) continue;

        
            if (locker.TryGetComponent(out LockerBlue script)) {
                script.inputs.Add(directions[i]);
            }
            else if (locker.TryGetComponent(out KillerRoomLockerKiller lockerScript2)) {
                lockerScript2.inputs.Add(directions[i]);
            }
            rectTransform.anchoredPosition = startPosition;
            isDragging = false;
        }
    }
#endregion

#region Public Methods
    public void OnBeginDrag(PointerEventData eventData)
    {
        startMousePosition = eventData.position;
        offset = rectTransform.anchoredPosition - startMousePosition;
        isDragging = true;
        soundManager.Play("directionLock");
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector2 currentMousePosition = eventData.position;
        Vector2 delta = currentMousePosition - startMousePosition;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) {
            rectTransform.anchoredPosition = new Vector2(currentMousePosition.x + offset.x, rectTransform.anchoredPosition.y);
        }
        else {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, currentMousePosition.y + offset.y);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = startPosition;
        isDragging = false;
    }
#endregion
}