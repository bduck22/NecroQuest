using System.Collections.Generic;
using Unity.VisualScripting;

public class ItemBase
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

public class Acc : ItemBase
{
    public AccType AccType;
    public Acc(string Name, string Description)
    {
        
    }
}
public class Guardian : ItemBase
{
    public GuardianType GuardianType;
    public Guardian(GuardianType guardianType, string Name, string Description)
    {

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
    //public static readonly Dictionary<int, Acc> AccData = new Dictionary<int, Acc>()
    //{
    //    {0, new Acc() },
    //};
}

