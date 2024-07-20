using UnityEngine;
using UniRx.Triggers;
using UniRx;

public class IdolMove : MonoBehaviour
{
    public ReactiveProperty<Vector2> Input_Vector2 = new();
    float _speed;
    Rigidbody2D _rigidbody;
    Character _character;
    void Start() => Init();

    void Init()
    {
        _character = transform.GetComponent<Character>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _character.Speed.Subscribe(Update_Speed).AddTo(this);
        this.FixedUpdateAsObservable().Subscribe(_ => Idol_Move());
    }

    void Update_Speed(float speed)
    {
        this._speed = speed;
    }
    void Idol_Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(horizontal, vertical);
        Input_Vector2.Value = input;
        _rigidbody.MovePosition(transform.position + input * _speed * Time.fixedDeltaTime);
    }
}
