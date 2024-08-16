using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class ItemBox : MonoBehaviour
{
    Character _character;
    IDisposable _disposable;
    Camera _camera;
    GameObject _chestPointer;
    BoxCollider2D _boxCollider;

    private void Awake()
    {
        _boxCollider = Utils.GetOrAddComponent<BoxCollider2D>(gameObject);
        _boxCollider.isTrigger = true;
        _boxCollider.offset = new Vector2(0, 2);
        _boxCollider.size = new Vector2(34, 19f);
        _character = Manager.Game.Character;
        _camera = Camera.main;
        _chestPointer = transform.Find("ChestPointer").gameObject;
    }


    public void Init(Transform newPos)
    {
        transform.position = newPos.position;
        _disposable?.Dispose();
        _disposable = this.OnTriggerEnter2DAsObservable().Subscribe(Ontrigger);
        SetLayer();
    }

    void SetLayer()
    {
        SpriteRenderer sp;
        sp = GetComponent<SpriteRenderer>();
        sp.drawMode = SpriteDrawMode.Simple;
        sp.sprite = Manager.Asset.LoadSprite("spr_holozonBox_0");
        sp.sortingOrder = 2;
        transform.localScale = new Vector2(0.03f, 0.03f);

        sp = transform.Find("Effect").GetComponent<SpriteRenderer>();
        sp.transform.localScale = Vector2.one;
        sp.sortingOrder = 1;

        sp = transform.Find("ChestPointer").GetComponent<SpriteRenderer>();
        sp.transform.localScale = new Vector2(0.5f, 0.5f);
        sp.sortingOrder = 3;
    }
    void Ontrigger(Collider2D collider)
    {
        if (collider.CompareTag(Define.Tag.IDOL))
        {
            Manager.UI.ShowPopup<Popup_ItemBox>();
            Manager.Spawn.ItemBox.Release(this);
        }
    }

    private void Update()
    {
        CheckIfOutsideCameraView();
    }

    private void CheckIfOutsideCameraView()
    {
        Vector3 viewportPosition = _camera.WorldToViewportPoint(transform.position);
        bool isOutside = viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1;

        if (isOutside)
        {
            UpdateArrowPosition();
        }
        else
        {
            _chestPointer.transform.localPosition = new Vector2(0, 85);
            _chestPointer.transform.localRotation = new Quaternion(0, 0, -90, 0);

        }
    }

    private void UpdateArrowPosition()
    {

        Vector3 viewportPosition = _camera.WorldToViewportPoint(transform.position);

        float arrowPadding = 0.1f;
        Vector3 arrowPosition = viewportPosition;

        arrowPosition.x = Mathf.Clamp(arrowPosition.x, arrowPadding, 1 - arrowPadding);
        arrowPosition.y = Mathf.Clamp(arrowPosition.y, arrowPadding, 1 - arrowPadding);

        _chestPointer.transform.position = _camera.ViewportToWorldPoint(arrowPosition);
        _chestPointer.transform.position = new Vector3(_chestPointer.transform.position.x, _chestPointer.transform.position.y, 0);

        Vector3 middlePoint = (_character.transform.position + transform.position) / 2;
        Vector3 direction = middlePoint - _camera.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _chestPointer.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

}
