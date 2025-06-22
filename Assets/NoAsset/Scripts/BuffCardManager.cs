using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuffCardManager : MonoBehaviour
{
    public Transform[] Cards;
    public Guardian[] carddata;
    int[] keys;
    public int Loadcount;
    Text LoadButton;
    private void Awake()
    {
        Cards = new Transform[] { transform.GetChild(0), transform.GetChild(1), transform.GetChild(2) };
        LoadButton = transform.GetChild(3).GetComponentInChildren<Text>();
    }
    private void OnEnable()
    {
        Loadcount = 100;
        CardLoad();
    }
    public void Load()
    {

    }

    public void CardLoad()
    {
        carddata = new Guardian[Cards.Length];
        keys = new int[Cards.Length];
        Loadcount--;
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

        if(Loadcount > 0)
        {
            LoadButton.gameObject.SetActive(true);
            LoadButton.text = "새로고침 " + Loadcount;
        }
        else LoadButton.transform.parent.gameObject.SetActive(false);

    }
    public void CardSelect(int number)
    {
        PlayerManager.instance.guardians.Add(carddata[number]);
        PlayerManager.instance.GuardianLoad();
    }
    private bool Check(int R, int m)
    {
        for (int i = 0; i < keys.Length; i++) {
            if (keys[i] == R&&i!=m)
            {
                return false;
            }
        }
        foreach (Guardian g in PlayerManager.instance.guardians)
        {
            if(g.GuardianType== Data.GuardianData[R].GuardianType)
            {
                return false;
            }
        }
        return true;
    }
}
