using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ExpID
{
    None
}
public class ExpData : MonoBehaviour
{
    public ExpID ID { get; set; }
    public float Exp { get; set; }
}
