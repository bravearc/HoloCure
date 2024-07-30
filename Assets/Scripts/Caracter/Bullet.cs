using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IAttack
{
    Rigidbody2D _rd;
    Transform _target;
    float _speed = 5f;
    float _damage = 12f;

    float _lifeTime = 5f;


    void Start()
    {
        _rd = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Target").transform;
        Init();
    }
    private void Init()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        _rd.AddForce(direction * _speed, ForceMode2D.Impulse);

        Destroy(gameObject, _lifeTime);
    }

    public float TakeAttack()
    {
        return _damage;
    }
}
