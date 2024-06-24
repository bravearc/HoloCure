using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D _rd;
    Transform _target;
    float _speed = 5f;
    void Start()
    {
        _rd = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Target").transform;
    }
    private void OnEnable()
    {
        Vector3 direction = (transform.position - _target.position).normalized;
        _rd.velocity = direction * _speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rd.rotation = angle;
    }

    public void TargetSet(Transform target)
    {
        this._target = target;
    }
    private void FixedUpdate()
    {
        _rd.MovePosition(Vector2.right);
    }
}
