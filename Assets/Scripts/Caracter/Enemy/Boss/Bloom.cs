using UnityEngine;

public class Bloom : Boss_Base
{
    Vector2 size = new Vector2(0.07f, 0.07f);
    protected override void SetBoss()
    {
        _enemy.SetBoss(size);
    }

}
