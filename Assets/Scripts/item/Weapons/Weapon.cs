using UniRx;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public ReactiveProperty<int> Level { get; private set; } = new();
    public ItemID ID;
    public WeaponData WeaponData { get; private set; }
    public virtual void Init(ItemID id)
    {
        WeaponData = Manager.Data.Weapon[id][0];
    }

    public virtual void LevelUp()
    {
        ++Level.Value;
    }


}
