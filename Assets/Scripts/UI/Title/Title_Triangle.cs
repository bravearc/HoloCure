using System;
using System.Security.Cryptography;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Title_Triangle : UI_SubItem
{
    IDisposable _disposable;
    public Vector2[] _spawnPositions = new Vector2[10];
    float[] _speed;
    float[] _size;
    int multiplication = 100;
    RectTransform[] _triangles;
    void Start() => Init();

    protected override void Init()
    {
        base.Init();
        _triangles = new RectTransform[transform.childCount];
        _speed = new float[transform.childCount];
        _size = new float[transform.childCount];
        for (int idx = 0; idx < _spawnPositions.Length; idx++)
        {
            _spawnPositions[idx] = new Vector2(-900 + idx * 200, 650);
        }
        for (int idx = 0; idx < _triangles.Length; idx++)
        {
            _triangles[idx] = transform.GetChild(idx).GetComponent<RectTransform>();
        }
        _disposable = this.UpdateAsObservable().Subscribe(_ => TriangleRotate());
    }

    void TriangleRotate()
    {

        for (int idx = 0; idx < _triangles.Length; idx++)
        {
            if (_triangles[idx].anchoredPosition.y <= -600)
            {
                Vector2 position = _spawnPositions[UnityEngine.Random.Range(0, 10)];
                _speed[idx] = UnityEngine.Random.Range(0.5f, 1.5f);
                _size[idx] = UnityEngine.Random.Range(70, 200);
                _triangles[idx].anchoredPosition = position;
            }

            _triangles[idx].sizeDelta = new Vector2(_size[idx], _size[idx]);
            _triangles[idx].Rotate(Vector3.forward * _speed[idx] * multiplication * Time.deltaTime);
            _triangles[idx].anchoredPosition -= new Vector2(0, _speed[idx]);
        }
    }
}
