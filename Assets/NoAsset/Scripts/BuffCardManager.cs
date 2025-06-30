using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffCardManager : MonoBehaviour
{
    public Transform[] Cards;
    public Guardian[] carddata;
    public int[] keys;
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

    public List<int> gets = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
    public void CardLoad()
    {
        if(gets.Count == 0)
        {
            Time.timeScale = 1;
            transform.parent.gameObject.SetActive(false);
            return;
        }
        carddata = new Guardian[Cards.Length];
        keys = new int[Cards.Length];
        Loadcount--;
        for (int i = 0; i < 3; i++)
        {

            if (gets.Count == 0)
            {
                Cards[i].gameObject.SetActive(false);
            }
            else
            {
                int R = Random.Range(0, gets.Count);
                R = gets[R];
                keys[i] = R;
                carddata[i] = Data.GuardianData[keys[i]];

                Cards[i].gameObject.SetActive(true);
                Cards[i].GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>((carddata[i].GuardianType).ToString());
                Cards[i].GetChild(1).GetComponentInChildren<Text>().text = carddata[i].Name;
                Cards[i].GetChild(2).GetComponent<Text>().text = carddata[i].Description;

                gets.Remove(R);
            }
        }

        if (Loadcount > 0)
        {
            LoadButton.gameObject.SetActive(true);
            LoadButton.text = "새로고침 " + Loadcount;
        }
        else LoadButton.transform.parent.gameObject.SetActive(false);

    }
    public void Init()
    {
        for (int i = 0; i < 3; i++)
        {
            gets.Add(keys[i]);
        }
    }
    public void CardSelect(int number)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i != number&&carddata[number].Name!="")
            {
                gets.Add(keys[i]);
            }
        }
        Time.timeScale = 1;
        PlayerManager.instance.guardians.Add(carddata[number]);
        PlayerManager.instance.GuardianLoad();
    }
}
