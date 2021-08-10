using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MultiPlayerRandomItemSettings")]
public class RandomItemSettings : ScriptableObject
{
    [SerializeField] int attackItemRandomRate;
    [SerializeField] int defenceItemRandomRate;
    [SerializeField] int abnormalItemRandomRate;
    [SerializeField] int boostCanItemRandomRate;
    [SerializeField] int dropBackItemRandomRate;


    public int GetAttackItemRandomRate()
    {
        return attackItemRandomRate;
    }

    public int GetDefenceItemRandomRate()
    {
        return defenceItemRandomRate;
    }

    public int GetAbnormalItemRandomRate()
    {
        return abnormalItemRandomRate;
    }

    public int GetBoostCanItemRandomRate()
    {
        return boostCanItemRandomRate;
    }

    public int GetDropBackItemRandomRate()
    {
        return dropBackItemRandomRate;
    }

    public int GetTotalRate()
    {
        return attackItemRandomRate + defenceItemRandomRate + abnormalItemRandomRate + boostCanItemRandomRate + dropBackItemRandomRate;
    }
}
