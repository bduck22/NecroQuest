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
    Charge,
    Provo,
    Spirit,
    Moral2,
    Moral4
}

public enum Attack_Type
{
    longRange,
    ShotRange
}

[System.Serializable]
public class Buff
{
    public Buff_Type Type;
    public int Value;
    public float Time;
    public Transform Target;
    public bool Run=true;

    public int Value2;
    public Buff(Buff_Type Type, int value, float time)
    {
        this.Type = Type;
        this.Value = value;
        this.Time = time;
    }
    public Buff(Buff_Type Type, int value1, int value2, float time)
    {
        this.Type = Type;
        this.Value = value1;
        this.Time = time;
    }
    public Buff(Buff_Type Type, Transform Target, float time)
    {
        this.Type = Type;
        this.Target = Target;
        this.Time = time;
    }
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
    }

    public int Diffi;

    public int Wave;
    public Wave[] Waves;

    public GameStatus GameStatus;

    SpawnManager SpawnManager;

    [Header("BuffEffects")]
    public Transform[] BuffEffects;

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
                StageStart();
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
    void StageStart()
    {
        GameStatus = GameStatus.WaveStart;
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
        for (int i = 0; i < PlayerManager.instance.Units.Length; i++)
        {
            PlayerManager.instance.Units[i].UnitInit();
        }
    }

    void Result()
    {
        Debug.Log("스테이지 끝");
    }
}
