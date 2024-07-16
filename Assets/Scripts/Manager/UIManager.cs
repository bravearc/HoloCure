using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    int _order = -20;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("UI_Root");
            if(root == null)
            {
                root = new GameObject { name = "UI_Root" };
            }

            return root;
        }
    }
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = sort;

        if (sort)
        {
            canvas.sortingOrder = _order;
            ++_order;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_SubItem
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject prefab = Manager.Asset.Load<GameObject>(Manager.Asset.Object, $"UI/SubItem/{name}");

        GameObject go = Manager.Asset.Instantiate(prefab);
        if (parent != null)
            go.transform.SetParent(parent, false);


        go.transform.localScale = Vector3.one;
        go.transform.localPosition = prefab.transform.position;
        go.transform.localRotation = Quaternion.identity;

        return Utils.GetOrAddComponent<T>(go);
    }

    public T ShowPopup<T>(string name = null, Transform parent = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject prefab = Manager.Asset.Load<GameObject>(Manager.Asset.Object, $"UI/Popup/{name}");

        GameObject go = Manager.Asset.Instantiate($"UI/Popup/{name}");
        T popup = Utils.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        if (parent != null)
            go.transform.SetParent(parent);
        else 
            go.transform.SetParent(Root.transform);

        go.transform.localScale = Vector3.one;
        go.transform.localPosition = prefab.transform.position;

        return popup;
    }

    public T FindPopup<T>() where T : UI_Popup
    {
        return _popupStack.Where(x => x.GetType() == typeof(T)).FirstOrDefault() as T;
    }

    public T PeekPopupUI<T>() where T : UI_Popup
    {
        if (_popupStack.Count == 0)
            return null;
        return _popupStack.Peek() as T;
    }

    public void ClosePopupUI() 
    {
        if (_popupStack.Count == 0)
        { 
            return;
        }
        UI_Popup popup = _popupStack.Pop();
        Manager.Asset.Destroy(popup.gameObject);
        --_order;

    }

    public void CloseALLPopupUI()
    {
        while(_popupStack.Count > 0) 
        { 
            ClosePopupUI();
        }
    }

    public void Clear()
    {
        CloseALLPopupUI();
    }
}
