using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.XR;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using Unity.Burst.Intrinsics;

public class NameDEscriptionBase
{
    public string Name;
    public string Description;
    public void Set(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
public enum GuardianType
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    OneOne,
    OneTwo,
    OneThree,
    OneFour,
    OneFive
}
[System.Serializable]
public class MobStat : NameDEscriptionBase
{
    public float Hp;
    public float Speed;
    public float Damage;
    public float AttackSpeed;
    public float Intersection;
    public MobStat(float Hp, float Speed, float Damage, float AttackSpeed, float Intersection)
    {
        this.Hp = Hp;
        this.Speed = Speed;
        this.Damage = Damage;
        this.AttackSpeed = AttackSpeed;
        this.Intersection = Intersection;
    }
}

[System.Serializable]
public class Guardian : NameDEscriptionBase
{
    public GuardianType GuardianType;
    public UnitStats Stats = new UnitStats();
    public Guardian(GuardianType guardianType, string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
        this.GuardianType = guardianType;
        switch (guardianType)
        {
            case GuardianType.One:
                Stats.SkillCool += 1;
                Stats.SetValue += 0.2f;
                break;
            case GuardianType.Two:
                break;
            case GuardianType.Three:
                break;
            case GuardianType.Four:
                break;
            case GuardianType.Five:
                break;
            case GuardianType.Six:
                break;
            case GuardianType.Seven:
                break;
            case GuardianType.Eight:
                break;
            case GuardianType.Nine:
                break;
            case GuardianType.Ten:
                break;
            case GuardianType.OneOne:
                break;
            case GuardianType.OneTwo:
                break;
            case GuardianType.OneThree:
                break;
            case GuardianType.OneFour:
                break;
            case GuardianType.OneFive:
                break;
        }
    }
}

[System.Serializable]
public class UnitData : NameDEscriptionBase
{
    public float Hp;
    public float Speed;
    public float Damage;
    public float AttackSpeed;
    public float Cooltime;
    public UnitData(float Hp, float Speed, float Damage, float AttackSpeed, float Cooltime, string Name, string Description, string sName, string sDesc, string pName, string pDesc)
    {
        this.Hp = Hp;
        this.Speed = Speed;
        this.Damage = Damage;
        this.AttackSpeed = AttackSpeed;

        this.Name = Name;
        this.Description = Description;
        this.Cooltime = Cooltime;

        Skill.Set(sName, sDesc);
        Passive.Set(pName, pDesc);
    }
    public NameDEscriptionBase Skill=new NameDEscriptionBase();
    public NameDEscriptionBase Passive = new NameDEscriptionBase();
}

public static class Data
{
    public static readonly Dictionary<int, Guardian> GuardianData = new Dictionary<int, Guardian>()
    {
        {0, new Guardian((GuardianType)0, "마법친화", "1설명") },
        {1, new Guardian((GuardianType)1, "2이름", "2설명") },
        {2, new Guardian((GuardianType)2, "3이름", "3설명") },
        {3, new Guardian((GuardianType)3, "4이름", "4설명") },
        {4, new Guardian((GuardianType)4, "5이름", "5설명") },
        {5, new Guardian((GuardianType)5, "6이름", "6설명") },
        {6, new Guardian((GuardianType)6, "7이름", "7설명") },
        {7, new Guardian((GuardianType)7, "8이름", "8설명") },
        {8, new Guardian((GuardianType)8, "9이름", "9설명") },
        {9, new Guardian((GuardianType)9, "10이름", "10설명") },
        {10, new Guardian((GuardianType)10, "11이름", "11설명") },
        {11, new Guardian((GuardianType)11, "12이름", "12설명") },
        {12, new Guardian((GuardianType)12, "13이름", "13설명") },
        {13, new Guardian((GuardianType)13, "14이름", "14설명") },
        {14, new Guardian((GuardianType)14, "15이름", "15설명") }
    };
    public static readonly Dictionary<MobType, MobStat> MobData = new Dictionary<MobType, MobStat>()
    {
        {MobType.Zombie, new MobStat(2, 1.5f, 1, 0, 0) },
        {MobType.Skull, new MobStat(1.5f, 1.5f, 1, 1, 7) },
        {MobType.Ghost, new MobStat(1, 10f, 1, 0, 12) },
        {MobType.Shade, new MobStat(2, 5, 15, 0.5f, 0) },
        {MobType.Ghoul, new MobStat(2.5f, 2, 1, 1, -1.5f) },
        {MobType.Dullahan, new MobStat(15, 5, 10, 0.5f, 0) },
        {MobType.Necro, new MobStat(50, 2, 5, 1, 2.5f) }
    };
    public static readonly Dictionary<UnitClass, UnitData> UnitData = new Dictionary<UnitClass, UnitData>()
    {
        {UnitClass.GuardN, new UnitData( 1, 1, 1, 1, 1, "수호기사", "근거리 캐릭터\n" +
            "받는 피해량 감소\n" +
            "도발디버프", 
            "도발의 함성",
            "일정범위 내의 모든 적에게 100%의공격력만큼 피해를 주고 5초 동안 유지되는 도발디버프를 건다.\n" +
            "(도발 : 타겟팅을 이 유닛으로 바꿈)",
            "강인한 육체",
            "받는 피해량 -25%") },
        {UnitClass.HolyM, new UnitData( 1, 1, 1, 1, 1, "신관", "원거리 캐릭터\n" +
            "압도적인 회복량\n" +
            "\"공격불가\" \n추천하지 않음",
            "치유의 파동",
            "일정범위 내의 모든 아군의 체력을 150%의공격력만큼 회복시킨다.",
            "불살",
            "신관의 기본공격은 적을 공격하는 대신 체력이 가장 낮은 아군의 체력을 100%의공격력만큼 회복시킨다.") },
        {UnitClass.SpiritM, new UnitData( 1, 1, 1, 1, 1, "정령사", "원거리 캐릭터\n" +
            "범용성 높음\n" +
            "낮은 기본 능력치",
            "정령의가호",
            "현재 체력이 가장 낮은 아군에게 n초동안 n의 이동속도 증가시키고 체력을 n만큼 회복시킨다.",
            "4대정령",
            "스테이지 시작 시 모든 아군이 정령사의 가장 높은 능력치의 20%를 획득한다.") },
        {UnitClass.Berserker, new UnitData( 1, 1, 1, 1, 1, "광전사", "근거리 캐릭터\n" +
            "높은 기본 능력치\n" +
            "추천하지 않음",
            "광분",
            "광전사가 분노하여 n초 동안 n의 이동속도가 증가하고, 기본공격이 강화된다.",
            "발악",
            "광전사는 자신의 잃은 체력에 비례해서 공격력과 공격속도가 증가한다.\n" +
            "40당 0.5의 공격력 | 60당 0.5의 공격속도") },
        {UnitClass.ArchM, new UnitData( 1, 1, 1, 1, 1, "대마법사", "원거리 캐릭터\n" +
            "강력한 순간 화력\n" +
            "낮은 초당 데미지",
            "메테오",
            "대마법사가 메테오를 날려 일정 범위 내에 100%의공격력+5%의마력만큼의 피해를 입힌다.",
            "마력집중",
            "대마법사가 기본공격을 할 떄마다 마력이 1씩 쌓인다.\n" +
            "마력 1당 메테오의 범위가 2%상승") },
        {UnitClass.Archer, new UnitData( 1, 1, 1, 1, 1, "궁수", "원거리 캐릭터\n" +
            "넓은 공격범위\n" +
            "추천",
            "매지컬 샷",
            "궁수가 현재 방향으로 거대한 화살을 날려 100%의공격력만큼의 피해를 입힌다.",
            "크리티컬 샷",
            "궁수의 기본공격은 30%의 확률로 피해량이 50%증가한다.") },
        {UnitClass.DragonN, new UnitData( 1, 1, 1, 1, 1, "용기사", "근거리 캐릭터\n" +
            "끈질긴 생명력\n" +
            "긴 선딜레이",
            "드래곤 러쉬",
            "용기사가 잠시 기를 모아 현재 방향으로 돌진한다. 일정범위 내의 적에게 100%의공격력만큼 피해르 입힌다.",
            "흡혈재생",
            "용기사가 입힌 피해의 n%만큼 회복한다.") }
    };
    public static List<int> Units = new List<int>();
    public static UnitStats Stats;

    public static LocalData LocalData;
    public static string path = Path.Combine(Application.dataPath, "LocalData.json");
    public static string characterpath = Path.Combine(Application.dataPath, "Characters.json");
    public static string blessingpath = Path.Combine(Application.dataPath, "Blessings.json");
    public static string presetpath = Path.Combine(Application.dataPath, "Presets.json");

    public static void Save()
    {
        string json = JsonUtility.ToJson(LocalData, false);
        File.WriteAllText(path, json);
        string json1 = Json.DicToJson(LocalData.GetUnits, false);
        File.WriteAllText(characterpath, json1);
        string json2 = Json.DicToJson(LocalData.Blessing, false);
        File.WriteAllText(blessingpath, json2);
        string json3 = Json.DoubleListToJson(LocalData.Presets, false);
        File.WriteAllText(presetpath, json3);

    }
    public static void Load()
    {
        Units = new List<int>();
        Stats = new UnitStats();
        LocalData = new LocalData();

        string loadJson = File.ReadAllText(path);
        LocalData = JsonUtility.FromJson<LocalData>(loadJson);

        string loadJson1 = File.ReadAllText(characterpath);
        LocalData.GetUnits = Json.JsonToDic<UnitClass, LocalUnit>(loadJson1);

        string loadJson2 = File.ReadAllText(blessingpath);
        LocalData.Blessing = Json.JsonToDic<BlessingType, int>(loadJson2);

        string loadJson3 = File.ReadAllText(presetpath);
        LocalData.Presets = Json.JsonToDoubleList<int>(loadJson3);

        foreach (UnitClass u in LocalData.GetUnits.Keys)
        {
            Units.Add((int)u);
        }
    }
}

[Serializable]
public class LocalData
{
    public int SelectPreSet;
    public int diffi ;
    public bool First;
    public Dictionary<UnitClass, LocalUnit> GetUnits;//보유유닛
    public int Gold;//골드
    public Dictionary<BlessingType, int> Blessing;//축복
    public UnitClass StartingUnit;//스타팅유닛
    public List<int[]> Presets;//프리셋
    public float Master=1;
    public float SFX=1;
    public float BGM=1;
}
[Serializable]
public class LocalUnit
{
    public int level;
    public float Damage;
    public float AttackSpeed;
    public float Hp;
    public float Speed;
    public LocalUnit()
    {
        level = 0;
        Damage = 0;
        AttackSpeed = 0;
        Hp = 0;
        Speed = 0;
    }
}
[SerializeField]
public enum BlessingType
{
    Attack,
    Defence,
    Skill,
    Moral
}
public static class Json
{
    public static string DicToJson<TKey, TValue>(Dictionary<TKey, TValue> DicData, bool pretty)
    {
        List<DataDictionary<TKey, TValue>> datalist = new List<DataDictionary<TKey, TValue>>();
        DataDictionary<TKey, TValue> data;
        foreach (TKey key in DicData.Keys)
        {
            data = new DataDictionary<TKey, TValue>();
            data.Key = key;
            data.Value = DicData[key];
            datalist.Add(data);
        }
        DataArray<TKey, TValue> arraydata = new DataArray<TKey, TValue>();
        arraydata.data = datalist;

        return JsonUtility.ToJson(arraydata, pretty);
    }
    public static string DoubleListToJson<TKey>(List<TKey[]> list, bool pretty)
    {
        List<DataDictionary<int, TKey>> datalist = new List<DataDictionary<int, TKey>>();
        DataDictionary<int, TKey> data;
        for (int i = 0; i < list.Count; i++)
        {
            foreach (TKey key in list[i])
            {
                data = new DataDictionary<int, TKey>();
                data.Key = i;
                data.Value = key;
                datalist.Add(data);
            }
        }
        DataArray<int, TKey> arraydata = new DataArray<int, TKey>();
        arraydata.data = datalist;

        return JsonUtility.ToJson(arraydata, pretty);
    }
    public static List<TKey[]> JsonToDoubleList<TKey>(string json)
    {
        DataArray<int, TKey> datalist = JsonUtility.FromJson< DataArray<int, TKey>>(json);

        List<TKey[]> list = new List<TKey[]>();

        int n = 0;
        List<TKey> arr = new List<TKey>();
        for (int i = 0; i < datalist.data.Count; i++)
        {
            if (n != datalist.data[i].Key)
            {
                list.Add(arr.ToArray());
                arr = new List<TKey>();
                n = datalist.data[i].Key;
            }
            arr.Add(datalist.data[i].Value);
        }
        list.Add(arr.ToArray());

        return list;
    }
    public static Dictionary<TKey, TValue> JsonToDic<TKey, TValue>(string json)
    {
        DataArray < TKey, TValue> datalist = JsonUtility.FromJson< DataArray < TKey, TValue>> (json);
        
        Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();

        for(int i = 0; i < datalist.data.Count; i++)
        {
            DataDictionary<TKey, TValue> data = datalist.data[i];
            dic[data.Key] = data.Value;
        }

        return dic;
    }
}
[Serializable]
public class DataDictionary<TKey, TValue>
{
    public TKey Key;
    public TValue Value;
}

[Serializable]
public class DataArray<TKey, TValue>
{
    public List<DataDictionary<TKey, TValue>> data;
}