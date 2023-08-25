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

    [Header("�ڹ��� ������Ʈ")]
    public GameObject locker;

    [Header("ȭ�鿡 ������ �Է� �ؽ�Ʈ")]
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
        // �巡�װ� ���۵� �� ó���� ����
        startMousePosition = eventData.position;
        offset = rectTransform.anchoredPosition - startMousePosition;
        isDragging = true;
        SoundManager.instance.SFXPlay("directionLock");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        // �巡�� �� ó���� ����
        Vector2 currentMousePosition = eventData.position;
        Vector2 delta = currentMousePosition - startMousePosition;

        // X�� ��ȭ�� Y�� ��ȭ �� ũ�Ⱑ ū ���� �����Ͽ� ������
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            rectTransform.anchoredPosition = new Vector2(currentMousePosition.x + offset.x, rectTransform.anchoredPosition.y);
        else
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, currentMousePosition.y + offset.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // �巡�װ� ���� �� ó���� ����
        rectTransform.anchoredPosition = startPosition;
        isDragging = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < directions.Count; i++)
        {
            if (collision.transform.name == directions[i])
            {
                // ���ϴ� ��ũ��Ʈ�� �����ͼ� ����մϴ�.
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
                    Debug.LogWarning("Locker ��ũ��Ʈ�� ã�� �� �����ϴ�.");
                }
            }
        }
    }
}
