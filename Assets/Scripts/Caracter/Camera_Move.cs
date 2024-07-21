using UniRx.Triggers;
using UniRx;
using UnityEngine;
using System;

public class Camera_Move : MonoBehaviour
{
    IDisposable _disposable;
    public Character _character;
    public bool _isPlaying;
    private void Start()
    {
        Manager.Game.IsPlaying.Subscribe(IsPlay).AddTo(this);
    }
    void Init()
    {
        _character = Manager.Game.Character;
        _disposable = this.LateUpdateAsObservable().Subscribe(_ => CameraMove());
    }

    void IsPlay(bool isPlay) 
    { 
        _isPlaying = isPlay;

        if (_isPlaying)
            Init();
        else
        {
            _disposable?.Dispose();
            _disposable = null;
        }
    }
    void CameraMove()
    {
        if (_isPlaying)
        {
            Vector3 newPosition = _character.transform.position;
            transform.position = new Vector3(newPosition.x, newPosition.y, -10);
        }
    }

}
