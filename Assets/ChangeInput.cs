using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ChangeInput : MonoBehaviour
{
    EventSystem system;

    [Header("ó�� ������ InputField")]
    public Selectable firstInput;

    [Header("���� �Է� �� ���� ��ư")]
    public Button submitBtn;


    void Start()
    {
        system = EventSystem.current;
        // ó���� �̸��� Input Field�� �����ϵ��� �Ѵ�.
        firstInput.Select();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            // Tab + LeftShift�� ���� Selectable ��ü�� ����
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Tab�� �Ʒ��� Selectable ��ü�� ����
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            // ����Ű�� ġ�� ��ư�� Ŭ��
            submitBtn.onClick.Invoke();
        }
    }
}