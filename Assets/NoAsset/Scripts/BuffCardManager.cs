using System.Collections.Generic;
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
        Loadcount = 3;
        CardLoad();
    }

    public List<int> gets = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
    public void CardLoad()
    {
        carddata = new Guardian[Cards.Length];
        keys = new int[Cards.Length];
        Loadcount--;
        for (int i = 0; i < 3; i++)
        {
            int R = Random.Range(0, gets.Count);
            if (gets.Count == 0)
            {
                Cards[i].GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                keys[i] = gets[R];
                carddata[i] = Data.GuardianData[keys[i]];

                Cards[i].GetChild(0).gameObject.SetActive(true);
                Cards[i].GetChild(0).GetComponent<Image>().sprite = null;
                Cards[i].GetChild(1).GetComponentInChildren<Text>().text = carddata[i].Name;
                Cards[i].GetChild(2).GetComponent<Text>().text = carddata[i].Description;

                gets.RemoveAt(R);
            }

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
}
