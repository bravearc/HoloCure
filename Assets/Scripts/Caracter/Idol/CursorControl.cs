using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CursorControl : MonoBehaviour
{
    Rigidbody2D playerRbody;
    Transform _pointer;
    Transform[] _points;
    Transform _atPoint;
    Transform _pointPosition;
    SpriteRenderer _cursor;

    float _axisHor;
    float _axisVer;
    float _speed = 3f;

    public bool _isMouse;

    private void Start() => Init();

    void Init()
    {
        _pointer = transform.Find("Pointer");
        _atPoint = transform.Find("AttackPointers");
        _cursor = _pointer.GetComponent<SpriteRenderer>();
        _points = new Transform[_atPoint.childCount];
        for (int i = 0; i < 8; i++)
        {
            _points[i] = _atPoint.GetChild(i).transform;
        }
        _pointPosition = _points[3];

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
        if (!_isMouse)
        {
            _cursor.color = Color.white;

            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                PointerMove(_points[2]);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                PointerMove(_points[4]);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                PointerMove(_points[0]);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                PointerMove(_points[6]);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                PointerMove(_points[7]);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                PointerMove(_points[1]);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                PointerMove(_points[5]);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                PointerMove(_points[3]);
            }
        }

        else
        {
            _cursor.color  = Color.yellow;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector3 direction = (mousePosition - transform.position).normalized;
            float distance = Vector3.Distance(mousePosition, transform.position);
            float maxDistance = 1.5f;

            if (distance > maxDistance)
            {
                mousePosition = transform.position + direction * maxDistance;
            }
            _pointer.transform.position = mousePosition;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _pointer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        void PointerMove(Transform tr)
        {
            _pointer.parent = tr;
            _pointPosition = tr;
            _pointer.localPosition = Vector3.zero;
            _pointer.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }
}