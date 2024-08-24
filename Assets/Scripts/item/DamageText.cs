using System.Collections;
using TMPro;
using UnityEngine;
public enum DamageType
{
    Player,
    Enemy,
    Critical
}
public class DamageText : MonoBehaviour
{
    TMP_Text _damageText;
    float _endPositionY = 1;
    IEnumerator _moveCo;
    float _dieTime = 0.5f;
    void Awake()
    {
        _damageText = Utils.FindChild<TMP_Text>(gameObject, nameof(TMP_Text));
    }

    public void Init(float damage, Vector2 newPos, DamageType type)
    {
        transform.position = newPos;

        Color color = type switch
        {
            DamageType.Player => Color.red,
            DamageType.Enemy => Color.white,
            DamageType.Critical => Color.yellow,
            _ => Color.white
        };;

        _damageText.text = ((int)damage).ToString();
        _damageText.color = color;
        _moveCo = MoveCo();
        StartCoroutine(_moveCo);
    }

    IEnumerator MoveCo()
    {
        float time = 0;
        Vector2 startPos = transform.position;
        Vector2 endPos = new Vector2(transform.position.x, transform.position.y + _endPositionY);
        while (time <= _dieTime)
        {
            transform.position = Vector2.Lerp(startPos, endPos, time);

            time += Time.deltaTime;
            yield return null;
        }
        Die();
    }
    void Die()
    {
        Manager.Spawn.DamageText.Release(this);
    }
}
