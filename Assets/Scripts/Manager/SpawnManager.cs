using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private EnemyID enemyID;

    public void Init()
    {
        
    }
    public GameObject BossRezen(int i)
    {
        BossData bossName = (BossData)i;
        return Manager.Asset.LoadObject(bossName.ToString());
    }

    public GameObject EnemyRezen(int i)
    {
        EnemyID enemy = (EnemyID)i;
        return Manager.Asset.LoadObject(enemy.ToString());

    }
    public GameObject ItemBox()
    {
        IItem type = RandomType();
        if (Manager.Game.Inventory.IsItemFull(type)) 
        {
            return RandomItem(type);
        }
        else 
        {
            ItemBox();
        }
        return null;
    }


    public void LevelUp()
    {

    }

    private IItem RandomType()
    {
        int random = Random.Range(1, 4);
        if (random == 1)
        {
            return new Weapon();
        }
        else if (random == 2) 
        {
            return new Equipment();
        }
        else
        {
            return new Stemp();
        }
    }
    private GameObject RandomItem<T>(T t)
    {
        #region 랜덤 범위 책정
        int minInt = 1;
        int maxInt = 0;
        switch (t)
        {
            case Weapon:
                maxInt = Manager.Data.Weapon.Count + 1;
                break;
            case Equipment:
                maxInt = Manager.Data.Equipment.Count + 1;
                break;
            case Stemp:
                maxInt = Manager.Data.Stemp.Count + 1;
                break;
            default:
                break;
        }
        #endregion

        int itemNumber = Random.Range(minInt, maxInt);
        if (t is not Stemp)
        {
            int dataTableNumber = 6;
            itemNumber = (itemNumber == 0) ? 1 : itemNumber * dataTableNumber;
        }

        return DicFilter(t, itemNumber);
    }

    private GameObject DicFilter<T>(T t, int i)
    {
        if (t is Weapon)
        {
            return Manager.Asset.LoadObject(Manager.Data.Weapon[(WeaponID)i].Name);
        }
        else if (t is Equipment)
        {
            return Manager.Asset.LoadObject(Manager.Data.Equipment[(EquipmentID)i].Name);
        }
        else if (t is Stemp) 
        {
            return Manager.Asset.LoadObject(Manager.Data.Stemp[(StempID)i].Name);
        }
        return null; 
    }
}
