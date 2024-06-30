using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float _hp = 30f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            IAttack attack =  collision.gameObject.GetComponent<IAttack>();
            _hp -= attack.TakeAttack();
            Destroy(collision.gameObject);
            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }


}
