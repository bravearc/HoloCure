using System.Collections;
using UnityEngine;

public class PlayDice : WeaponMultishot
{
    Vector2 size = new Vector2(3, 3);
    protected override void WeaponSetComponenet(Attack attack)
    {
        int idx = Random.Range(0, 6);
        attack.SetSprite(Manager.Asset.LoadSprite($"spr_BaeDice_{idx}"));

        attack.SetAttackComponent(false, size, Vector2.one, Vector2.zero);
        StartCoroutine(MoveStopCo(attack));
    }
    IEnumerator MoveStopCo(Attack attack)
    {
        Vector2 startPos = attack.transform.position;
        Vector2 currentPos = startPos;
        float targetDistance = 2f;
        while (true)
        {
            currentPos = attack.transform.position;
            float movePos = Vector2.Distance(startPos, currentPos);

            if (movePos > targetDistance)
                break;
            yield return null;
            
        }

        Rigidbody2D rd = attack.GetRigid();
        rd.velocity = Vector2.zero;
    }
}
