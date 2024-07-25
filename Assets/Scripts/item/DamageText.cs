using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    Text _damageText;
    float _endPositionY = 3;
    IEnumerator _moveCo;
    void Start()
    {
        _damageText = Utils.FindChild<Text>(gameObject, nameof(Text));
        _moveCo = MoveCo();
    }

    public void Init(float damage, Transform tr)
    {
        transform.position = tr.position;
        _damageText.text = ((int)damage).ToString();
        StartCoroutine(_moveCo);
    }

    IEnumerator MoveCo()
    {
        float time = 0;
        Vector2 startPos = transform.position;
        Vector2 endPos = new Vector2(transform.position.x, transform.position.y + _endPositionY);
        while (time >= 1)
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
