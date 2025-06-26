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
    Shade,
    Dullahan,
    Necro,
}
public enum GameStatus
{
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
    Moral5,
    Berserk,
    BerserkP
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
    public float Value;
    public float Time;
    public Transform Target;
    public bool Run = true;
    public bool Loop = false;

    public float Value2;
    public Buff(Buff_Type Type, float value, float time, bool loop)
    {
        this.Type = Type;
        this.Value = value;
        this.Time = time;
        Loop = loop;
    }
    public Buff(Buff_Type Type, float value1, float value2, float time, bool loop)
    {
        this.Type = Type;
        this.Value = value1;
        this.Value2 = value2;
        this.Time = time;
        Loop = loop;
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
    public bool middle;
    public bool final;
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

    public int gold;

    public int Diffi;

    public int Wave;
    public Wave[] Waves;

    public GameStatus GameStatus;

    SpawnManager SpawnManager;

    public Transform WaveStartButton;

    [Header("BuffEffects")]
    public Transform[] BuffEffects;

    private void Start()
    {
        GameStatus = GameStatus.StageStart;
        SpawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
    }

    private void Update()
    {
        switch (GameStatus)
        {
            case GameStatus.StageStart:
                StageStart();
                break;
            case GameStatus.WaveStart:
                WaveStart();
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
        PlayerManager.instance.StageStart();
    }

    public void WaveStart()
    {
        one = true;
        GameStatus = GameStatus.Waving;
        PlayerManager.instance.UnitsInit();
        SpawnManager.WaveStart();
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
    bool one=true;
    void Rest()
    {
        if (one)
        {
            one = false;
            float R = Random.Range(0.0f, 1.0f);
            if(R < 0.9f)
            {
                GuardianSelecter.gameObject.SetActive(true);
                Time.timeScale = 0;
                //GuardianSelecter.Load();
            }
            else
            {
                WaveStartButton.gameObject.SetActive(true);
            }
        }
        //GameStatus = GameStatus.WaveStart;
        //for (int i = 0; i < PlayerManager.instance.Units.Length; i++)
        //{
        //    PlayerManager.instance.Units[i].UnitInit();
        //}
    }

    void Result()
    {
        Debug.Log("스테이지 끝");
    }

    public void Resume()
    {
        WaveStartButton.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}
