using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CursorControl : MonoBehaviour
{
    Transform _cursor;
    SpriteRenderer _cursorSprite;

    Vector3[] _positions = new Vector3[8]
    {
        new Vector3(-0.973f, 0.832f, 145),
        new Vector3(0, 1.28f, 90),
        new Vector3(0.973f, 0.857f, 45),
        new Vector3(1.3f, 0f, 0),
        new Vector3(0.973f, -0.917f, -45),
        new Vector3(0f, -1.28f, -90),
        new Vector3(-0.973f, -0.943f, -145),
        new Vector3(-1.3f, 0f, 180)
    };

    public bool _isMouse;

    private void Start() => Init();

    void Init()
    {
        _cursor = transform.Find("Cursor");
        _cursorSprite = _cursor.GetComponent<SpriteRenderer>();

        this.UpdateAsObservable().Subscribe(_ => ProcessInput());
    }

    void ProcessInput()
    {
        MouseCheck();
        TargetPoint();
    }

    void MouseCheck()
    {
        if(Input.GetMouseButtonDown(0)) 
        { 
            _isMouse = !_isMouse;
        }
    }
    void TargetPoint()
    {
        if (_isMouse == false)
        {
            _cursorSprite.color = Color.white;

            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                PointerMove(2);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                PointerMove(4);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                PointerMove(0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                PointerMove(6);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                PointerMove(7);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                PointerMove(1);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                PointerMove(5);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                PointerMove(3);
            }
        }

        else
        {
            _cursorSprite.color  = Color.yellow;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector3 direction = (mousePosition - transform.position).normalized;
            float distance = Vector3.Distance(mousePosition, transform.position);
            float maxDistance = 1.5f;

            if (distance > maxDistance)
            {
                mousePosition = transform.position + direction * maxDistance;
            }
            _cursor.transform.position = mousePosition;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _cursor.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        void PointerMove(int idx)
        {
            Vector2 newPosition = new Vector2(_positions[idx].x, _positions[idx].y);
            float rotation = _positions[idx].z;

            _cursor.localPosition = newPosition;
            _cursor.localRotation = Quaternion.Euler(0f, 0f, rotation);
        }
    }
}