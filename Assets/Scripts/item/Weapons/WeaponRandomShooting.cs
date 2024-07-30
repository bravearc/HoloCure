using System.Collections;
using UnityEngine;

public class WeaponRandomShooting : WeaponMultishot
{
    protected override void AttackAction(Attack attack)
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy closestEnemy = enemies[0];
        float closestDistance = Vector3.Distance(_character.transform.position, closestEnemy.transform.position);

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(_character.transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        _cursor = closestEnemy.transform;

        base.AttackAction(attack);
    }
}
