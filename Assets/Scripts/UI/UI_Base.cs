// ----- C#
using System;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using TMPro;

public class UI_Base : MonoBehaviour
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    Dictionary<Type,UnityEngine.Object[]> _object = new Dictionary<Type, UnityEngine.Object[]>();
    
    // --------------------------------------------------
    // Functions - Normal
    // --------------------------------------------------
    protected void Bind<T>(Type type) where T: UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        Debug.Log(names[0]);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _object.Add(typeof(T),objects);

        for(int i=0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Utils.FindChild(gameObject,names[i],true);
            else
                objects[i] = Utils.FindChild<T>(gameObject,names[i],true);
            if (objects[i] == null)
                Debug.LogError($"[UI_Base] 해당 UI의 바인드를 실패하였습니다.({names[i]})");
        }
    }
    
    protected T Get<T>(int idx) where T: UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        _object.TryGetValue(typeof(T), out objects);
        if (_object.TryGetValue(typeof(T), out objects) == false)
            return null;
        return objects[idx] as T;
    }

    protected TextMeshProUGUI GetText(int idx) => Get<TextMeshProUGUI>(idx);
    protected Button GetButton(int idx) => Get<Button>(idx);
    protected Image GetImage(int idx) => Get<Image>(idx);

    public static void AddUIEvent(GameObject go, Action<PointerEventData> action, Define.EUIEvent type = Define.EUIEvent.Click)
    {
        UI_EventHandler evt = go.GetOrAddComponent<UI_EventHandler>();
        switch(type)
        {
            case Define.EUIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;

            case Define.EUIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case Define.EUIEvent.Drop:
                evt.OnDropHandler -= action;
                evt.OnDropHandler += action;
                break;
            case Define.EUIEvent.PointUp:
                evt.OnPointerUpHandler -= action;
                evt.OnPointerUpHandler += action;
                break;
        }
    }
}