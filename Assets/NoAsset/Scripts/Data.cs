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
        {0, new Guardian((GuardianType)0, "1�̸�", "1����") },
        {1, new Guardian((GuardianType)1, "2�̸�", "2����") },
        {2, new Guardian((GuardianType)2, "3�̸�", "3����") },
        {3, new Guardian((GuardianType)3, "4�̸�", "4����") },
        {4, new Guardian((GuardianType)4, "5�̸�", "5����") },
        {5, new Guardian((GuardianType)5, "6�̸�", "6����") },
        {6, new Guardian((GuardianType)6, "7�̸�", "7����") },
        {7, new Guardian((GuardianType)7, "8�̸�", "8����") },
        {8, new Guardian((GuardianType)8, "9�̸�", "9����") },
        {9, new Guardian((GuardianType)9, "10�̸�", "10����") },
    };
    //public static readonly Dictionary<int, Acc> AccData = new Dictionary<int, Acc>()
    //{
    //    {0, new Acc() },
    //};
}

