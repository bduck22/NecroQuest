using JetBrains.Annotations;
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
public enum MobType
{
    Zombie,
    Skull,
    Ghost,
    Ghoul,
    Spider,
    Shade,
    Lich,
    Dullahan,
    Necro,
}
public enum GameStatus
{
    Main,
    Lobby,
    Organ,
    StageStart,
    WaveStart,
    Waving,
    WaveEnd,
    Rest,
    Result
}

public enum Buff_Type
{
    Charge
}

[System.Serializable]
public struct Buff
{
    public Buff_Type Type;
    public int Value;
    public float Time;
}

[System.Serializable]
public struct Mob
{
    [Header("Stats")]
    public float MaxHp;
    public float Hp;
    public float Speed;
    public float Damage;
    public List<Buff> Buff;

    [Header("Type")]
    public MobType Type;
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
    public List<Buff> Buff;

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

    private void Awake()
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

    public GameStatus GameStatus;

    SpawnManager SpawnManager;

    private void Start()
    {
        GameStatus = GameStatus.WaveStart;
        SpawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
    }

    private void Update()
    {
        switch (GameStatus)
        {
            case GameStatus.Main:
                break;
            case GameStatus.Lobby:
                break;
            case GameStatus.Organ:
                break;
            case GameStatus.StageStart:
                break;
            case GameStatus.WaveStart:
                WaveStart();
                break;
            case GameStatus.Waving:
                Waving();
                break;
            case GameStatus.WaveEnd:
                WaveEnd();
                break;
            case GameStatus.Rest:
                Rest();
                break;
            case GameStatus.Result:
                Result();
                break;
        }
    }
    void WaveStart()
    {
        SpawnManager.WaveStart();
        GameStatus = GameStatus.Waving;
    }

    void Waving()
    {

    }

    void WaveEnd()
    {
        if (++Wave >= Waves.Length)
        {
            GameStatus = GameStatus.Result;
        }
        else GameStatus = GameStatus.Rest;
    }

    void Rest()
    {
        GameStatus = GameStatus.WaveStart;
    }

    void Result()
    {
        Debug.Log("스테이지 끝");
    }
}
