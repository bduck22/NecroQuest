using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
                Stats.SkillCool -= 3;
                Stats.SetValue += 0.2f;
                break;
            case GuardianType.Two:
                Stats.Damage+=23;
                Stats.MoralUp += 0.2f;
                Stats.Speed -= 0.4f;
                break;
            case GuardianType.Three:
                Stats.Speed += 1.2f;
                Stats.SkillDamage+=0.25f;
                break;
            case GuardianType.Four:
                Stats.Hp += 2;
                Stats.SetValue += 0.25f;
                break;
            case GuardianType.Five:
                Stats.AttackSpeed += 0.7f;
                Stats.SkillCool -=2;
                break;
            case GuardianType.Six:
                Stats.Speed += 1.3f;
                Stats.SkillDamage +=0.1f;
                Stats.GetHeal -= 0.15f;
                break;
            case GuardianType.Seven:
                Stats.Damage += 12;
                Stats.AttackSpeed += 1.6f;
                Stats.GetHeal -= 0.4f;
                Stats.Speed -= 1.2f;
                break;
            case GuardianType.Eight:
                Stats.GetDamage -= 0.3f;
                Stats.AttackSpeed -= 0.6f;
                break;
            case GuardianType.Nine:
                Stats.Intersection += 4;
                Stats.AttackSpeed -= 0.6f;
                break;
            case GuardianType.Ten:
                Stats.SkillCool-=1.2f;
                Stats.SkillDamage+=0.1f;
                Stats.GetHeal += 0.1f;
                break;
            case GuardianType.OneOne:
                Stats.GetHeal += 0.15f;
                Stats.MoralUp += 0.1f;
                break;
            case GuardianType.OneTwo:
                Stats.Speed += 3;
                Stats.GetDamage += 0.2f;
                break;
            case GuardianType.OneThree:
                Stats.AttackSpeed += 1.8f;
                Stats.GetHeal -= 0.2f;
                Stats.MoralUp -= 0.2f;
                break;
            case GuardianType.OneFour:
                Stats.Hp += 1.8f;
                Stats.Speed += 0.4f;
                Stats.AttackSpeed += 1.6f;
                Stats.SkillDamage += 0.05f;
                Stats.MoralUp -= 0.7f;
                break;
            case GuardianType.OneFive:
                Stats.MoralUp += 0.2f;
                Stats.Speed -= 0.3f;
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
        {0, new Guardian((GuardianType)0, "마법친화", "스킬 쿨타임 -3초\n가하는 피해 및 회복량 +20%") },
        {1, new Guardian((GuardianType)1, "그을린 의지", "공격력 +23\n획득 사기량+20%\n이동속도 -0.4") },
        {2, new Guardian((GuardianType)2, "유성의 흐름", "이동속도 +1.2\n스킬 피해량 +25%") },
        {3, new Guardian((GuardianType)3, "유혈흡수", "최대체력 +2\n가하는 피해 및 회복량 +25%") },
        {4, new Guardian((GuardianType)4, "신묘한힘", "공격속도 +0.7\n스킬 쿨타임 -2초") },
        {5, new Guardian((GuardianType)5, "잿빛의 잔재주", "이동속도 +1.3\n스킬 피해량 +10%\n받는 치유량 -15%") },
        {6, new Guardian((GuardianType)6, "결의맺은 공성", "공격력 +12\n공격속도 +1.6\n받는 회복량 -40%\n이동속도 -1.2") },
        {7, new Guardian((GuardianType)7, "경질화", "받는 피해량 -30%\n공격속도 -0.6") },
        {8, new Guardian((GuardianType)8, "장거리 투사", "사거리 +4\n공격속도 -0.4") },
        {9, new Guardian((GuardianType)9, "마력 순항", "스킬 쿨타임 -1.2초\n스킬 피해량 +10%\n받는 회복량 +10%") },
        {10, new Guardian((GuardianType)10, "요정의 포옹", "받는 회복량 +15%\n획득 사기량 +10%") },
        {11, new Guardian((GuardianType)11, "신속", "이동속도 +3.0\n받는 피해량 +20%") },
        {12, new Guardian((GuardianType)12, "과로", "공격속도 +1.8\n받는 회복량 -20%\n획득 사기량 -20%") },
        {13, new Guardian((GuardianType)13, "자력갱생", "체력 +3.5\n이동속도 +0.4\n공격속도 +1.6\n스킬 피해량 +5%") },
        {14, new Guardian((GuardianType)14, "강건한 고양", "획득 사기량 +20%\n이동속도 -0.3") }
    };
    public static readonly Dictionary<MobType, MobStat> MobData = new Dictionary<MobType, MobStat>()
    {
        {MobType.Zombie, new MobStat(1, 1.5f, 1, 0, 0) },
        {MobType.Skull, new MobStat(1, 1, 1.5f, 1, 7) },
        {MobType.Ghost, new MobStat(0.5f, 14, 1.5f, 0, 12) },
        {MobType.Shade, new MobStat(1.75f, 3, 10, 0.5f, 0) },
        {MobType.Ghoul, new MobStat(2, 1.5f, 2.5f, 1, -1.5f) },
        {MobType.Dullahan, new MobStat(15, 7, 10, 0.5f, 0) },
        {MobType.Necro, new MobStat(22, 5, 15, 1, 2.5f) }
    };
    public static readonly Dictionary<UnitClass, UnitData> UnitData = new Dictionary<UnitClass, UnitData>()
    {
        {UnitClass.GuardN, new UnitData( 3.0f, 1.5f, 1.5f, 0.5f, 15f,"수호기사", "근거리 캐릭터\n" +
            "받는 피해량 감소\n" +
            "도발디버프",
            "도발의 함성",
            "일정범위 내의 모든 적에게 (체력75%)의 피해(100%)를 주고 3초 동안 도발한다.\n" +
            "(도발 : 타겟팅을 이 유닛으로 바꿈)",
            "강인한 육체",
            "받는 피해량 -15%") },
        {UnitClass.HolyM, new UnitData( 1.5f, 2f, 2f, 1f, 17f,  "신관", "원거리 캐릭터\n" +
            "압도적인 회복량\n" +
            "\"공격불가\" \n추천하지 않음",
            "치유의 파동",
            "일정범위 내의 모든 아군의 체력을 (공격력20%+이동속도100%) 회복(200%)시킨다.",
            "불살",
            "신관의 기본공격은 적을 공격하는 대신 체력이 가장 낮은 아군의 체력을 100%의공격력만큼 회복(50%)시킨다.") },
        {UnitClass.SpiritM, new UnitData( 2.0f, 2f, 1f, 1f, 10f, "정령사", "원거리 캐릭터\n" +
            "범용성 높음\n" +
            "낮은 기본 능력치",
            "정령의가호",
            "현재 체력이 가장 낮은 아군에게 3초동안 (대상의 현재 이동속도35%)의 이동속도를 증가시키고 체력을 (이동속도100%)만큼 회복(150%)시킨다.",
            "4대정령",
            "스테이지 시작 시 모든 아군이 정령사의 가장 높은 능력치의 25%를 획득한다.") },
        {UnitClass.Berserker, new UnitData( 2.5f, 1.5f, 2f, 1f, 15f, "광전사", "근거리 캐릭터\n" +
            "높은 기본 능력치\n" +
            "추천하지 않음",
            "광폭화",
            "광전사가 분노하여 6초 동안 1.5의 이동속도가 증가하고, 기본공격이 강화된다.",
            "발악",
            "광전사는 자신의 잃은 체력에 비례해서 공격력과 공격속도가 증가한다.\n" +
            "20당 0.5의 공격력 0.75 30당 0.25의 공격속도") },
        {UnitClass.ArchM, new UnitData( 1.5f, 1.5f, 2f, 1.5f, 20f, "대마법사", "원거리 캐릭터\n" +
            "강력한 순간 화력\n" +
            "낮은 초당 데미지",
            "메테오",
            "대마법사가 메테오를 날려 일정 범위 내에 (공격력100%+마력5%) 피해(300%)를 입힌다.",
            "마력집중",
            "대마법사가 기본공격을 할 떄마다 마력이 1씩 쌓인다.\n" +
            "마력 1당 메테오의 범위가 2%상승") },
        {UnitClass.Archer, new UnitData( 1f, 1.5f, 2.5f, 1.5f, 12f, "궁수", "원거리 캐릭터\n" +
            "넓은 공격범위\n" +
            "추천",
            "매지컬 샷",
            "궁수가 현재 방향으로 거대한 화살을 날려 (공격력50%+이동속도75%) 피해(200%)를 입힌다.",
            "크리티컬 샷",
            "궁수의 기본공격은 20%의 확률로 기본공격 피해량이 50%증가한다.") },
        {UnitClass.DragonN, new UnitData( 2, 2, 1, 1, 10, "용기사", "근거리 캐릭터\n" +
            "끈질긴 생명력\n" +
            "긴 선딜레이",
            "드래곤 러쉬",
            "용기사가 잠시 기를 모아 현재 방향으로 돌진한다. 일정범위 내의 적에게 (공격속도100%) 피해(250%)를 입힌다.",
            "흡혈재생",
            "용기사가 입힌 피해의 30%만큼 회복한다.") }
    };
    public static List<int> Units = new List<int>();
    public static UnitStats Stats;

    public static LocalData LocalData;
    public static string path = Path.Combine(Application.dataPath, "LocalData.json");

    public static void Save()
    {
        string json = JsonUtility.ToJson(LocalData, false) + ";" + Json.DicToJson(LocalData.GetUnits, false) + ";" +Json.DicToJson(LocalData.Blessing, false) + ";" + Json.DoubleListToJson(LocalData.Presets, false);
        File.WriteAllText(path, json);
    }
    public static void Load()
    {
        Units = new List<int>();
        Stats = new UnitStats();
        LocalData = new LocalData();
        Data.LocalData.Presets = new List<int[]>();
        Data.LocalData.GetUnits = new Dictionary<UnitClass, LocalUnit>();
        Data.LocalData.Blessing = new Dictionary<BlessingType, int>()
            {   {BlessingType.Attack, 0 },
                {BlessingType.Defence, 0 },
                { BlessingType.Skill, 0 },
                {BlessingType.Moral, 0 }
            };
        Data.LocalData.Master = 1;
        Data.LocalData.SFX = 1;
        Data.LocalData.BGM = 1;

        string loadJson = File.ReadAllText(path);
        string[] Jsons = loadJson.Split(';');
        LocalData = JsonUtility.FromJson<LocalData>(Jsons[0]);
        LocalData.GetUnits = Json.JsonToDic<UnitClass, LocalUnit>(Jsons[1]);
        LocalData.Blessing = Json.JsonToDic<BlessingType, int>(Jsons[2]);
        LocalData.Presets = Json.JsonToDoubleList<int>(Jsons[3]);

        foreach (UnitClass u in LocalData.GetUnits.Keys)
        {
            Units.Add((int)u);
        }
    }
    public static void Delete()
    {
        File.Delete(path);
    }
}

[Serializable]
public class LocalData
{
    public int SelectPreSet;
    public int diffi ;
    public bool First;
    public Dictionary<UnitClass, LocalUnit> GetUnits;//��������
    public int Gold;//���
    public Dictionary<BlessingType, int> Blessing;//�ູ
    public UnitClass StartingUnit;//��Ÿ������
    public List<int[]> Presets;//������
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