using UnityEngine;

public class Player_Move : MonoBehaviour
{
    Rigidbody2D playerRbody;
    public Transform _pointer;
    public Transform[] _points;
    public Transform _atPoint;
    public Transform _pointPosition;

    float _axisHor;
    float _axisVer;
    float _speed = 3f;

    public bool _isMouse;

    public float moveSpeed { get; set; }

    
    void Awake()
    {
        playerRbody = GetComponent<Rigidbody2D>();
        _pointer = transform.Find("Target");
        _atPoint = transform.Find("EnemySpawnPoints");
        _points = new Transform[_atPoint.childCount];
        for(int i = 0; i < 8; i++)
        {
            _points[i] = _atPoint.Find($"Point ({i})");
        }
        _pointPosition = _points[3];

    }

    private void Update()
    {
        MouseCheck();
        AttackPointChange();
    }

    void FixedUpdate()
    {
        _axisHor = Input.GetAxisRaw("Horizontal");
        _axisVer = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(_axisHor, _axisVer);

        if (move.sqrMagnitude > 1)
        {
            move.Normalize();
        }
        playerRbody.MovePosition(playerRbody.position + move * Time.fixedDeltaTime * _speed);
        _pointer.position = _pointPosition.position;
    }
    void MouseCheck()
    {
        if(Input.GetMouseButtonDown(0)) 
        { 
            _isMouse = !_isMouse;
        }
    }
    void AttackPointChange()
    {
        if (!_isMouse)
        {
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
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float closestDistance = float.MaxValue;
            Transform closestPoint = null;

            foreach (Transform point in _points)
            {
                float distance = Vector3.Distance(mousePosition, point.position);

                if (closestDistance > distance)
                {
                    closestDistance = distance;
                    closestPoint = point;
                }
            }
            PointerMove(closestPoint);
        }
    }
  

    void PointerMove(Transform tr)
    {
        _pointer.parent = tr;
        _pointPosition = tr;
        _pointer.localRotation = new Quaternion(0f, 0f, 0f, 0f);
    }
}