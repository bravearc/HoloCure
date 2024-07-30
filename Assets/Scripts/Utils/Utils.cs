using System;
using UnityEngine;
using Type = System.Type;


public class Utils : MonoBehaviour
{
    public static T ParseEnum<T>(string value, bool ignreCase = true)
    {
        return (T)Enum.Parse(typeof(T), value, ignreCase);
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            return go.AddComponent<T>();
        }
        return component;
    }
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
        {
            if (go == null)
            {
                return null;
            }

        if (recursive == false)
        {
            Transform transform = go.transform.Find(name);
            Debug.Assert(transform != null);
            return transform.GetComponent<T>();
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) == false && component.name != name) continue;
                    
                return component;
            }
        }
        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform != null)
            return transform.gameObject;
        return null;
    }

    public static bool IsWeapon(ItemID id)
    {
        if ((int)Define.ItemNumber.Weapon_Start <= (int)id
            && (int)id < (int)Define.ItemNumber.Weapon_End)
        {  
            return true; 
        }
        return false;
    }

    public static float GetRandomAngle()
    {
        return UnityEngine.Random.Range(-0.2f, 0.2f);
    }

    public static bool IsEquipment(ItemID id)
    {
        if((int)Define.ItemNumber.Equipment_Start <= (int)id
            && (int)id < (int)Define.ItemNumber.Equipment_End)
        {
            return true;
        }
        return false;
    }

    public static string GetItemType(ItemID id)
    {
        if ((int)id < (int)Define.ItemNumber.Weapon_End)
            return "Weapon";
        else if ((int)id < (int)Define.ItemNumber.Weapon_End)
            return "Equipment";
        else if ((int)Define.ItemNumber.Stats_Start <= (int)id
            && (int)id < (int)Define.ItemNumber.Stats_End)
            return "Stats";
        else
            return "Drop";
    }
}
