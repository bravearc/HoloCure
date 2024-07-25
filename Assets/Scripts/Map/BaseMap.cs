using UnityEngine;

public class BaseMap : MonoBehaviour
{
    protected GameObject _character;

    void Start() => Init();
    protected virtual void Init() 
    {
        _character = Manager.Game.Character.gameObject;
    }

}
