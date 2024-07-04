using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Stack<UIManager> BackStack;
    public void Init()
    {
        BackStack = new Stack<UIManager>();
    }

    public virtual void PushUI(UIManager ui)
    {
        ui.gameObject.SetActive(true);
        BackStack.Push(ui);
    }

    public virtual void PopUI() 
    {
        BackStack.Pop().gameObject.SetActive(false);
    }
    public virtual void ClearUI() 
    {
        if (BackStack.Count == 0)
        {
            Debug.Log("BackStack Count = 0");
            return;
        }
        BackStack?.Clear();
    }
}
