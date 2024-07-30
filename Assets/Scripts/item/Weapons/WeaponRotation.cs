using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponRotation : Weapon
{
    Vector2 _initPos;
    protected float _radius = 1.2f;
    float angleSpeed = 1f;
    //float initialAngle = 0f;
    protected override void AttackAction(Attack attack)
    {
        WeaponSetComponenet(attack);
        StartCoroutine(RotationCo(attack));
    }

    IEnumerator RotationCo(Attack attack) 
    { 
        Transform weapon = attack.GetComponent<Transform>();
        float initialAngle = 0f;
        
        float timer = 0f;
        while (true)
        {
            Vector2 pivotPoint = _character.transform.position;
            float currentAngle = initialAngle + angleSpeed * timer;

            float x = pivotPoint.x +  _radius * Mathf.Cos(currentAngle);
            float y = pivotPoint.y +  _radius * Mathf.Sin(currentAngle);
            attack.transform.position = new Vector2(x, y);

            timer += Time.fixedDeltaTime;
            yield return null;  
        }
    }

}
