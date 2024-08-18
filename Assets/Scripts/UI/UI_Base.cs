using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Type = System.Type;
using Object = UnityEngine.Object;
using UniRx;
using UnityEngine.EventSystems;
using UniRx.Triggers;

//[RequireComponent(typeof(Canvas))]
public abstract class UI_Base : MonoBehaviour
{
    Dictionary<Type, Object[]> _objects = new();
    void Start() => Init();

    protected virtual void Init()
    {
        
    }
    protected void Bind<T>(Type type) where T : Object
    {
        string[] names = Enum.GetNames(type);

        Object[] newObjects = new Object[names.Length];
        _objects.Add(typeof(T), newObjects);

        for (int i = 0; i < names.Length; i++) 
        { 
            if (typeof(T) == typeof(GameObject))
            {
                newObjects[i] = Utils.FindChild(gameObject, names[i], true);
            }
            else
            {
                newObjects[i] = Utils.FindChild<T>(gameObject, names[i], true);
            }

            if (newObjects[i] == null)
            {
                Debug.Log($"Failed to bind({names[i]})");
            }
        }
    }

    protected void BindObject(Type type) => Bind<GameObject>(type);
    protected void BindText(Type type) => Bind<Text>(type);
    protected void BindImage(Type type) => Bind<Image>(type);
    protected void BindButton(Type type) => Bind<Button>(type);
    protected void BindSlider(Type type) => Bind<Slider>(type);
    protected void BindAnimation(Type type) => Bind<Animation>(type);
    protected void BindAnimator(Type type) => Bind<Animator>(type);
    protected void BindTransform(Type type) => Bind<Transform>(type);
    protected void BindRectTransform(Type type) => Bind<RectTransform>(type);

    protected T Get<T>(int idx) where T : Object
    {
        if (_objects.TryGetValue(typeof(T), out Object[] objects))
        {
            return objects[idx] as T;
        }
        throw new InvalidOperationException($"Failed to Get({typeof(T)}, {idx}). Binding must be completed first.");
    }

    protected GameObject GetObject(int idx) => Get<GameObject>(idx);
    protected Text GetText(int idx) => Get<Text>(idx);
    protected Image GetImage(int idx) => Get<Image>(idx);
    protected Button GetButton(int idx) => Get<Button>(idx);
    protected Slider GetSlider(int idx) => Get<Slider>(idx);
    protected Animation GetAnimation(int idx) => Get<Animation>(idx);
    protected Animator GetAnimator(int idx) => Get<Animator>(idx);
    protected Transform GetTransform(int idx) => Get<Transform>(idx);
    protected RectTransform GetRectTransform(int idx) => Get<RectTransform>(idx);

    public static void BindEvent(UIBehaviour view, Action<PointerEventData> action, Define.UIEvent type , Component component)
    {
        switch (type)
        {
            case Define.UIEvent.Enter:
                view.OnPointerEnterAsObservable().Subscribe(action).AddTo(component);
                break;
            case Define.UIEvent.Click:
                view.OnPointerClickAsObservable().Subscribe(action).AddTo(component);
                break;
        }
    }
    public static void BindModelEvent<T>(ReactiveProperty<T> model, Action<T> action, Component component)
    {
        model.Subscribe(action).AddTo(component);
    }


}
