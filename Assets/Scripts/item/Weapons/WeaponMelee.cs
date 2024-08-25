using System.Collections;
using UnityEngine;

public class WeaponMelee : Weapon
{
    IEnumerator _positionCo;
    protected override void AttackAction(Attack attack)
    {
        base.AttackAction(attack);
        WeaponSetComponent(attack);
        attack.GetSprite().sortingOrder = 2;

        _positionCo = PositionCo(attack);
        StartCoroutine(_positionCo);
    }

    IEnumerator PositionCo(Attack attack)
    {

        attack.transform.position = (_character.transform.position + _cursor.position) / 2f;

        Vector3 previousCharacterPosition = _character.transform.position;

        while (attack.isActiveAndEnabled)
        {
            Vector3 characterMovement = _character.transform.position - previousCharacterPosition;

            attack.transform.position += characterMovement;

            previousCharacterPosition = _character.transform.position;

            yield return null;
        }
    }
    protected override void Disable(Attack attack)
    {
        StopCoroutine(_positionCo);
    }
}
