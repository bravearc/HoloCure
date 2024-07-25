using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extensions
{
    public static void BindEvent(this UIBehaviour view, Action<PointerEventData> action, Define.UIEvent type, Component component)
    => UI_Base.BindEvent(view, action, type, component);

    public static void BindModelEvent<T>(this ReactiveProperty<T> view, Action<T> action, Component component)
    => UI_Base.BindModelEvent<T>(view, action, component);

}

