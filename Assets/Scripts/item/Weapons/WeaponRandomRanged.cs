using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponRandomRanged : Weapon
{
    #region Component
    protected float _maxRange = 5f;
    IEnumerator _rangedCo;
    #endregion
    protected override void AttackAction(Attack attack)
    {
        WeaponSetComponent(attack);
        attack.GetSprite().sortingOrder = 1;
        
        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(0f, _maxRange);
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        Vector2 newPos = (Vector2)_character.transform.position + direction * distance;
        attack.transform.position = newPos;

        _rangedCo = RangedCo(attack);
        StartCoroutine(_rangedCo);

    }

    IEnumerator RangedCo(Attack attack)
    {
        float timer = 0;
        float attackTime = 2;

        while (true)
        {
            timer += Time.deltaTime;

            if (timer > attackTime)
            {
                attack.OffCollider();
                timer = 0;
            }
            else
            {
                attack.OnCollider();
            }

            yield return new WaitForSeconds(attack.HoldingTime);
        }
    }

    protected override void Disable(Attack attack)
    {
        StopCoroutine(_rangedCo);
    }

}
