using System.Collections.Generic;

public class NameDEscriptionBase
{
    public string Name;
    public string Description;
}
public enum AccType
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
    Ten
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
    Ten
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
    public UnitData(float Hp, float Speed, float Damage, float AttackSpeed, string Name, string Description)
    {
        this.Hp = Hp;
        this.Speed = Speed;
        this.Damage = Damage;
        this.AttackSpeed = AttackSpeed;
        this.Name = Name;
        this.Description = Description;
    }
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
    };
    public static readonly Dictionary<MobType, MobStat> MobData = new Dictionary<MobType, MobStat>()
    {
        {MobType.Zombie, new MobStat(2, 1.5f, 1, 0, 0) },
        {MobType.Skull, new MobStat(1.5f, 1.5f, 1, 1, 7) },
        {MobType.Ghost, new MobStat(1, 10f, 1, 0, 12) },
        {MobType.Shade, new MobStat(2, 5, 15, 0.5f, 0) },
        {MobType.Ghoul, new MobStat(2.5f, 2, 1, 1, -1.5f) },
        {MobType.Dullahan, new MobStat(15, 5, 10, 0.5f, 1f) }
    };
    public static readonly Dictionary<UnitClass, UnitData> UnitData = new Dictionary<UnitClass, UnitData>()
    {
        {UnitClass.GuardN, new UnitData(1, 1, 1, 1, "수호기사", "수호수호!") },
        {UnitClass.HolyM, new UnitData(1, 1, 1, 1, "신관", "수호수호!") },
        {UnitClass.SpiritM, new UnitData(1, 1, 1, 1, "정령사", "수호수호!") },
        {UnitClass.Berserker, new UnitData(1, 1, 1, 1, "광전사", "수호수호!") },
        {UnitClass.ArchM, new UnitData(1, 1, 1, 1, "대마법사", "수호수호!") },
        {UnitClass.Archer, new UnitData(1, 1, 1, 1, "궁수", "수호수호!") },
        {UnitClass.DragonN, new UnitData(1, 1, 1, 1, "용기사", "수호수호!") }
    };
    public static float Gold;
    public static int diffi;
    public static List<int> Units;
    public static UnitStats Stats;

    public static LocalData LocalData;
}
public class LocalData
{
    public Dictionary<UnitClass, LocalUnit> GetUnits = new Dictionary<UnitClass, LocalUnit>();//보유유닛
    public int Gold;//골드
    public Dictionary<BlessingType, int> Blessing = new Dictionary<BlessingType, int>();//축복
    public UnitClass StartingUnit;//스타팅유닛
    public List<int[]> Presets = new List<int[]>();//프리셋
}
public class LocalUnit
{
    public int level;
    public int Damage;
    public int AttackSpeed;
    public int Hp;
    public int Speed;
}
public enum BlessingType
{
    Attack,
    Defence,
    Skill,
    Moral
}
