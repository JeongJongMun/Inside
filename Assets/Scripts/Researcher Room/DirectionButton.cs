using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DirectionButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector3 startPosition;
    private Vector2 startMousePosition;
    private Vector2 offset;
    private bool isDragging;

    [Header("자물쇠 오브젝트")]
    public GameObject locker;

    [Header("화면에 보여줄 입력 텍스트")]
    public TMP_Text inputText;

    private List<string> directions = new List<string>() 
    { "Up", "Down", "Left", "Right" };

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그가 시작될 때 처리할 내용
        startMousePosition = eventData.position;
        offset = rectTransform.anchoredPosition - startMousePosition;
        isDragging = true;
        SoundManager.instance.SFXPlay("directionLock");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        // 드래그 중 처리할 내용
        Vector2 currentMousePosition = eventData.position;
        Vector2 delta = currentMousePosition - startMousePosition;

        // X축 변화와 Y축 변화 중 크기가 큰 축을 선택하여 움직임
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            rectTransform.anchoredPosition = new Vector2(currentMousePosition.x + offset.x, rectTransform.anchoredPosition.y);
        else
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, currentMousePosition.y + offset.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그가 끝날 때 처리할 내용
        rectTransform.anchoredPosition = startPosition;
        isDragging = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < directions.Count; i++)
        {
            if (collision.transform.name == directions[i])
            {
                // 원하는 스크립트를 가져와서 사용합니다.
                if (locker.TryGetComponent(out ResearcherRoomDrawerLocker1 lockerScript1))
                {
                    lockerScript1.inputs.Add(directions[i]);
                    inputText.text += directions[i] + " - ";
                    rectTransform.anchoredPosition = startPosition;
                    isDragging = false;
                    if (lockerScript1.inputs.Count == lockerScript1.answers.Count)
                    {
                        lockerScript1.TrySolve(locker);
                        lockerScript1.inputs.Clear();
                        inputText.text = "";
                    }
                }
                else if (locker.TryGetComponent(out KillerRoomLockerKiller lockerScript2))
                {
                    lockerScript2.inputs.Add(directions[i]);
                    inputText.text += directions[i] + " - ";
                    rectTransform.anchoredPosition = startPosition;
                    isDragging = false;
                    if (lockerScript2.inputs.Count == lockerScript2.answers.Count)
                    {
                        lockerScript2.TrySolve(locker);
                        lockerScript2.inputs.Clear();
                        inputText.text = "";
                    }
                }
                else
                {
                    Debug.LogWarning("Locker 스크립트를 찾을 수 없습니다.");
                }
            }
        }
    }
}
