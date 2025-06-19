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
    public UnitStats Stats;
    public Guardian(GuardianType guardianType, string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
        this.GuardianType = guardianType;
    }
}

public static class Data
{
    public static readonly Dictionary<int, Guardian> GuardianData = new Dictionary<int, Guardian>()
    {
        {0, new Guardian((GuardianType)0, "1이름", "1설명") },
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
        {MobType.Zombie, new MobStat(5, 1.5f, 1, 0, 0) },
        {MobType.Skull, new MobStat(5, 1.5f, 1, 1, 7) },
        {MobType.Ghost, new MobStat(3, 10f, 1, 0, 12) },
        {MobType.Shade, new MobStat(5, 5, 30, 0, 0) }
    };
    //public static readonly Dictionary<int, Acc> AccData = new Dictionary<int, Acc>()
    //{
    //    {0, new Acc() },
    //};
}