using System.Collections;
using UnityEngine;

public class TarotCards : WeaponMelee
{
    Vector2 size = new Vector2(0.5f, 0.2f);
    protected override void WeaponSetComponenet(Attack attack)
    {
        attack.SetAttackComponent(false, size, Vector2.one, Vector2.zero);
        StartCoroutine(Boomerang(attack));
    }

    IEnumerator Boomerang(Attack attack)
    {
        Vector2 characterPos = (Vector2)_character.transform.position + new Vector2(0,Utils.GetRandomAngle());
        Vector2 startPos = attack.transform.localPosition;
        Vector2 cursorDirection = ((Vector2)_cursor.position - characterPos).normalized;
        Vector2 endPos = startPos + cursorDirection * 4;
        float duration = 5f;
        float timer = 0f;

        while (true)
        {
            while (timer < duration)
            {
                attack.transform.localPosition = Vector2.Lerp(startPos, endPos, timer / duration);
                timer += Time.fixedDeltaTime;
                yield return null;
            }

            timer = 0f;

            while (timer < duration)
            {
                attack.transform.localPosition = Vector2.Lerp(endPos, startPos, timer / duration);
                timer += Time.fixedDeltaTime;
                yield return null;
            }

            timer = 0f;
        }
    }
}
