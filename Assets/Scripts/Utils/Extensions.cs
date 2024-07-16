using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extensions
{
    public static void BindEvent(this UIBehaviour view, Action<PointerEventData> action, Define.UIEvent type, Component component)
    => UI_Base.BindEvent(view, action, type, component);
}
