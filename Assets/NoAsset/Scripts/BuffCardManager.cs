using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuffCardManager : MonoBehaviour
{
    public Transform[] Cards;
    public Guardian[] carddata;
    UnitManager unitManager;
    int[] keys;
    void Start()
    {
        unitManager = PlayerManager.instance.UnitManager;
        Cards = new Transform[] { transform.GetChild(0), transform.GetChild(1) , transform.GetChild(2) };
        carddata = new Guardian[Cards.Length];
        keys = new int[Cards.Length];

        CardLoad();
    }
    public void CardLoad()
    {
        for (int i = 0; i < 3; i++)
        {
            int R = Random.Range(0, Data.GuardianData.Count);
            while (!Check(R, i))
            {
                R = Random.Range(0, Data.GuardianData.Count);
            }
            keys[i] = R;
            carddata[i] = Data.GuardianData[keys[i]];

            Cards[i].GetChild(0).GetComponent<Image>().sprite = null;
            Cards[i].GetChild(1).GetComponentInChildren<Text>().text = carddata[i].Name;
            Cards[i].GetChild(2).GetComponent<Text>().text = carddata[i].Description;
        }
    }
    public void CardSelect(int number)
    {
        unitManager.guardians.Add(carddata[number]);
    }
    private bool Check(int R, int m)
    {
        for (int i = 0; i < keys.Length; i++) {
            if (keys[i] == R&&i!=m)
            {
                return false;
            }
        }
        foreach (Guardian g in unitManager.guardians)
        {
            if(g.GuardianType== Data.GuardianData[R].GuardianType)
            {
                return false;
            }
        }
        return true;
    }
}
