using System.Collections.Generic;
using UnityEngine;

public enum UnitTargetType
{
    Close,
    Far,
    LowHp
}
public enum UnitClass
{
    GuardN,
    DragonN,
    HolyN,
    Fighter,
    Berserker,
    Archer,
    ArchM,
    SpiritM,
    HolyM
}
[System.Serializable]
public struct Mob
{
    [Header("Stats")]
    public float MaxHp;
    public float Hp;
    public float Speed;
    public float Damage;
    public List<int> Buffs;

    [Header("Type")]
    public UnitTargetType MobTargetType;
}

[System.Serializable]
public struct Unit_Base
{
    [Header("Stats")]
    public float Speed;
    public float AttackSpeed;
    public float Hp;
    public float MaxHp;
    public float Damage;
    public float Intersection;
    public float Moral;
    public List<int> Buffs;

    [Header("Type")]
    public UnitClass UnitClass;
    public UnitTargetType UnitTargetType;
}

[System.Serializable]
public struct Wave
{
    public Wave_Info[] MobInfo;
}

[System.Serializable]
public struct Wave_Info
{
    public int Type;
    public int Count;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int Wave;
    public Wave[] Waves;
}
