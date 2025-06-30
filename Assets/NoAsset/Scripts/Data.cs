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
                //Stats.SkillCool -= 1;
                Stats.SetValue += 0.2f;
                break;
            case GuardianType.Two:
                Stats.Damage+=23;
                Stats.MoralUp += 0.2f;
                Stats.Speed -= 0.4f;
                break;
            case GuardianType.Three:
                Stats.Speed += 1.2f;
                //Stats.SkillDamage+;
                break;
            case GuardianType.Four:
                Stats.Hp += 2;
                Stats.SetValue += 0.25f;
                break;
            case GuardianType.Five:
                Stats.AttackSpeed += 0.7f;
                //Stats.SkillCool -;
                break;
            case GuardianType.Six:
                Stats.Speed += 1.3f;
                //Stats.SkillDamage +;
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
                //Stats.SkillCool-;
                //Stats.SkillDamage+
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
                //Stats.SkillDamage+
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
        {0, new Guardian((GuardianType)0, "����ģȭ", "��ų ��Ÿ�� -��\n���ϴ� ���� �� ȸ���� +20%") },
        {1, new Guardian((GuardianType)1, "������ ����", "���ݷ� +23\nȹ�� ��ⷮ+20%\n�̵��ӵ� -0.4") },
        {2, new Guardian((GuardianType)2, "������ �帧", "�̵��ӵ� +1.2\n��ų ���ط� +%") },
        {3, new Guardian((GuardianType)3, "��������", "�ִ�ü�� +2\n�޴� ȸ���� + 100%") },
        {4, new Guardian((GuardianType)4, "�Ź�����", "���ݼӵ� +0.7\n��ų ��Ÿ�� -��") },
        {5, new Guardian((GuardianType)5, "������ ������", "�̵��ӵ� +1.3\n��ų ���ط� +%\n�޴� ġ���� -15%") },
        {6, new Guardian((GuardianType)6, "���Ǹ��� ����", "���� +12\n���ݼӵ� +1.6\n�޴� ȸ���� -40%\n�̵��ӵ� -1.2") },
        {7, new Guardian((GuardianType)7, "����ȭ", "�޴� ���ط�-30%\n���ݼӵ� -0.6") },
        {8, new Guardian((GuardianType)8, "��Ÿ� ����", "��Ÿ� +4\n���ݼӵ� -0.4") },
        {9, new Guardian((GuardianType)9, "���� ����", "��ų ��Ÿ�� -��\n��ų ���ط� +%\n�޴� ȸ���� +10%") },
        {10, new Guardian((GuardianType)10, "������ ����", "�޴� ȸ���� +15%\nȹ�� ��ⷮ +10%") },
        {11, new Guardian((GuardianType)11, "�ż�", "�̵��ӵ� +3.0\n�޴� ���ط� +20%") },
        {12, new Guardian((GuardianType)12, "����", "���ݼӵ� +1.8\n�޴� ȸ���� -20%\nȹ�� ��ⷮ -20%") },
        {13, new Guardian((GuardianType)13, "�ڷ°���", "ü�� +3.5\n�̵��ӵ� +0.4\n���ݼӵ� +1.6\n��ų ���ط� +30%") },
        {14, new Guardian((GuardianType)14, "������ ����", "ȹ�� ��ⷮ +20%\n�̵��ӵ� -0.3") }
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
        {UnitClass.GuardN, new UnitData( 3.0f, 1.5f, 1.5f, 0.5f, 15f, "��ȣ���", "�ٰŸ� ĳ����\n" +
            "�޴� ���ط� ����\n" +
            "���ߵ����", 
            "������ �Լ�",
            "�������� ���� ��� ������ 100%�ǰ��ݷ¸�ŭ ���ظ� �ְ� 5�� ���� �����Ǵ� ���ߵ������ �Ǵ�.\n" +
            "(���� : Ÿ������ �� �������� �ٲ�)",
            "������ ��ü",
            "�޴� ���ط� -25%") },
        {UnitClass.HolyM, new UnitData( 1.5f, 2f, 2f, 1f, 17f, "�Ű�", "���Ÿ� ĳ����\n" +
            "�е����� ȸ����\n" +
            "\"���ݺҰ�\" \n��õ���� ����",
            "ġ���� �ĵ�",
            "�������� ���� ��� �Ʊ��� ü���� 150%�ǰ��ݷ¸�ŭ ȸ����Ų��.",
            "�һ�",
            "�Ű��� �⺻������ ���� �����ϴ� ��� ü���� ���� ���� �Ʊ��� ü���� 100%�ǰ��ݷ¸�ŭ ȸ����Ų��.") },
        {UnitClass.SpiritM, new UnitData( 2.0f, 2f, 1f, 1f, 10f, "���ɻ�", "���Ÿ� ĳ����\n" +
            "���뼺 ����\n" +
            "���� �⺻ �ɷ�ġ",
            "�����ǰ�ȣ",
            "���� ü���� ���� ���� �Ʊ����� n�ʵ��� n�� �̵��ӵ� ������Ű�� ü���� n��ŭ ȸ����Ų��.",
            "4������",
            "�������� ���� �� ��� �Ʊ��� ���ɻ��� ���� ���� �ɷ�ġ�� 20%�� ȹ���Ѵ�.") },
        {UnitClass.Berserker, new UnitData( 2.5f, 1.5f, 2f, 1f, 15f, "������", "�ٰŸ� ĳ����\n" +
            "���� �⺻ �ɷ�ġ\n" +
            "��õ���� ����",
            "����",
            "�����簡 �г��Ͽ� n�� ���� n�� �̵��ӵ��� �����ϰ�, �⺻������ ��ȭ�ȴ�.",
            "�߾�",
            "������� �ڽ��� ���� ü�¿� ����ؼ� ���ݷ°� ���ݼӵ��� �����Ѵ�.\n" +
            "40�� 0.5�� ���ݷ� | 60�� 0.5�� ���ݼӵ�") },
        {UnitClass.ArchM, new UnitData( 1.5f, 1.5f, 2f, 1.5f, 20f, "�븶����", "���Ÿ� ĳ����\n" +
            "������ ���� ȭ��\n" +
            "���� �ʴ� ������",
            "���׿�",
            "�븶���簡 ���׿��� ���� ���� ���� ���� 100%�ǰ��ݷ�+5%�Ǹ��¸�ŭ�� ���ظ� ������.",
            "��������",
            "�븶���簡 �⺻������ �� ������ ������ 1�� ���δ�.\n" +
            "���� 1�� ���׿��� ������ 2%���") },
        {UnitClass.Archer, new UnitData( 1f, 1.5f, 2.5f, 1.5f, 12f, "�ü�", "���Ÿ� ĳ����\n" +
            "���� ���ݹ���\n" +
            "��õ",
            "������ ��",
            "�ü��� ���� �������� �Ŵ��� ȭ���� ���� 100%�ǰ��ݷ¸�ŭ�� ���ظ� ������.",
            "ũ��Ƽ�� ��",
            "�ü��� �⺻������ 30%�� Ȯ���� ���ط��� 50%�����Ѵ�.") },
        {UnitClass.DragonN, new UnitData( 2, 2, 1, 1, 10, "����", "�ٰŸ� ĳ����\n" +
            "������ ������\n" +
            "�� ��������",
            "�巡�� ����",
            "���簡 ��� �⸦ ��� ���� �������� �����Ѵ�. �������� ���� ������ 100%�ǰ��ݷ¸�ŭ ���ظ� ������.",
            "�������",
            "���簡 ���� ������ n%��ŭ ȸ���Ѵ�.") }
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
    public static void Delete()
    {
        File.Delete(path);
        File.Delete(characterpath);
        File.Delete(blessingpath);
        File.Delete(presetpath);
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