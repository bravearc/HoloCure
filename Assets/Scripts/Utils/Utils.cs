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
            if (transform != null)
            {
                return transform.GetComponent<T>();
            }
            else
            {
                foreach(T component in go.GetComponentsInChildren<T>()) 
                { 
                    if (string.IsNullOrEmpty(name) || component.name == name)
                    {
                        return component;
                    }
                }
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

    public static T Shuffle<T>(int min, int max)
    {
        int result = UnityEngine.Random.Range(min, max);

        return (T)Convert.ChangeType(result, typeof(T));
    }
}
