using System;
using System.Diagnostics;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
public class Title_ShowIdols : UI_SubItem
{
    void Start() => Init();
    IDisposable _updateDisposable;
    RectTransform[] _rects;
    private float[] _shakeY;
    private float[] _shakeDuration;
    private Vector2[] _initialPosition;

    protected override void Init()
    {
        //base.Init();
        Transform idols = transform;
        _rects = new RectTransform[idols.childCount];
        _shakeY = new float[idols.childCount];
        _shakeDuration = new float[idols.childCount];
        _initialPosition = new Vector2[idols.childCount];
        for (int idx = 0; idx < _rects.Length; idx++)
        {
            _rects[idx] = idols.GetChild(idx).GetComponent<RectTransform>();
            _initialPosition[idx] = _rects[idx].anchoredPosition;
            _shakeY[idx] = UnityEngine.Random.Range(30, 40);
            _shakeDuration[idx] = UnityEngine.Random.Range(1.5f, 2f);
        }
        _updateDisposable = this.UpdateAsObservable().
            Subscribe(_ => IdolsMove());

    }


    void IdolsMove()
    {

        for (int idx = 0; idx < _rects.Length; idx++)
        {

            Shake(_rects[idx], _initialPosition[idx], idx);
        }
    }

    private void Shake(RectTransform rect, Vector2 init, int idx)
    {

        float newY = Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI / _shakeDuration[idx])) * _shakeY[idx];

        Vector2 newPosition = init;
        newPosition.y += newY;

        rect.anchoredPosition = newPosition;
    }

    private void OnDisable()
    {
        _updateDisposable.Dispose();
    }
}
