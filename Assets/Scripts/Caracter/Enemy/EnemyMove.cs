using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    float _speed = 2f;
    Rigidbody2D _body;
    public Transform _player;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _player = Manager.Game.Character.transform;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        Vector3 movePosition = transform.position + direction * _speed * Time.deltaTime;
        _body.MovePosition(movePosition);
    }
}
