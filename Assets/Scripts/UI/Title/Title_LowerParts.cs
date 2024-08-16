using UnityEngine;

public class Title_LowerParts : MonoBehaviour
{
    public RectTransform _rect;
    Vector2 _endPoint = new Vector2(-70, 0);
    Vector2 _startPoint = new Vector2(0, 0);
    Vector2 _moveSpeed = new Vector2(-0.5f, 0);
    Vector2 _setPoint = new Vector2(-1300, 0);

    
    void Start() => Init();

    protected void Init()
    {
        _rect = GetComponent<RectTransform>();
        SetObject();
    }

    void SetObject()
    {
        for (int idx = 0; idx < 40; ++idx)
        {
            float range = 70f * idx;
            string path = "Title_LowerPart";
            GameObject go = Manager.Asset.Instantiate(path, transform);
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.anchoredPosition = _setPoint + new Vector2(range, 0);
        }
    }

    void Update()
    {
        _rect.anchoredPosition += _moveSpeed;
        if(_rect.anchoredPosition.x <= _endPoint.x)
        {
            _rect.anchoredPosition = _startPoint;
        }
    }
}
