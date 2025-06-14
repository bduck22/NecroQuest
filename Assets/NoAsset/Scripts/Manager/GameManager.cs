using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

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
    Moral1,
    Moral2,
    Moral4,
    Moral5
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
    public bool Run = true;

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

    //const string googlesheeturl = "https://docs.google.com/spreadsheets/d/12jlQL9fBaJSoOqOuuXTiVtZicH-X6jMGV56IdItUOHU/export?format=tsv&range=A2:G";

    //string sheetData;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    //IEnumerator Start()
    //{
    //    if (Application.internetReachability == NetworkReachability.NotReachable)
    //    {
    //        Debug.Log("인터넷 연결에 연결되지 않았습니다.");
    //    }
    //    else
    //    {
    //        Debug.Log("인터넷 연결에 연결되어 있습니다.");
    //        using (UnityWebRequest www = UnityWebRequest.Get(googlesheeturl))
    //        {
    //            yield return www.SendWebRequest();

    //            if (www.isDone)
    //            {
    //                sheetData = www.downloadHandler.text;
    //            }
    //        }
    //    }
    //}

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

    public void WaveStart()
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

    public Transform GuardianSelecter;
    void Rest()
    {
        //GameStatus = GameStatus.WaveStart;
        //for (int i = 0; i < PlayerManager.instance.Units.Length; i++)
        //{
        //    PlayerManager.instance.Units[i].UnitInit();
        //}
        Time.timeScale = 0;
        GuardianSelecter.gameObject.SetActive(true);
    }

    void Result()
    {
        Debug.Log("스테이지 끝");
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
