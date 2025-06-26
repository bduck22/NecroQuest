using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public enum Wannings
{
    Gold,
    Unit,
    Saved,
    MaxLv
}
public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        path = Path.Combine(Application.dataPath, "LocalData.json");
        StartCoroutine(Opening());
    }
    public Text WanningT;
    public Image Open;
    public Transform Starting;

    string path;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Data.Gold += 10000;
        }
    }
    bool iswanning = false;
    public void Wanning(Wannings Type)
    {
        string WText;
        switch (Type)
        {
            case Wannings.Gold:
                WText = "골드가 부족합니다.";
                if (WanningT.text == WText)
                {
                    iswanning = true;
                }
                else WanningT.text = WText;
                StartCoroutine(FadeOut(0));
                break;
            case Wannings.Unit:
                WText = "모든 용병을 획득했습니다.";
                if (WanningT.text == WText)
                {
                    iswanning = true;
                }
                else WanningT.text = WText;
                StartCoroutine(FadeOut(0));
                break;
            case Wannings.Saved:
                WText = "저장되었습니다.";
                if (WanningT.text == WText)
                {
                    iswanning = true;
                }
                else WanningT.text = WText;
                StartCoroutine(FadeOut(1));
                break;
            case Wannings.MaxLv:
                WText = "최대레벨입니다.";
                if (WanningT.text == WText)
                {
                    iswanning = true;
                }
                else WanningT.text = WText;
                StartCoroutine(FadeOut(1));
                break;
        }

    }
    IEnumerator FadeOut(int type)
    {
        if (iswanning)
        {
            yield return null;
        }

        WanningT.gameObject.SetActive(true);
        float value = 0.05f;
        if (type == 0)
        {
            WanningT.color = Color.red;
        }
        else
        {
            WanningT.color = Color.green;
        }

        yield return new WaitForSeconds(0.5f);
        while (WanningT.color.a >0)
        {
            WanningT.color -= Color.black * (value);
            yield return new WaitForSeconds(0.02f);
        }
        WanningT.gameObject.SetActive(false);

        iswanning = false;
    }
    IEnumerator Opening()
    {
        if (File.Exists(path))
        {
        }
        else
        {
            Data.Gold = 0;
            Data.diffi = 0;
            Data.Stats = new UnitStats();
            Data.Units = new List<int>();
            Data.LocalData = new LocalData();
            for (int i = 0; i < 3; i++)
            {
                Data.LocalData.Presets.Add(new int[4] { -1, -1, -1, -1 });
            }
            Starting.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        yield return new WaitForSeconds(0.5f);

        Open.gameObject.SetActive(true);
        float value = 1;
        Open.color = Color.black;
        yield return new WaitForSeconds(0.5f);
        while (Open.color.a > 0)
        {
            Open.color = Color.black * value;
            value -= 0.05f;
            yield return new WaitForSeconds(0.02f);
        }
        Open.gameObject.SetActive(false);
    }
    public UIGold UG;
    public bool UseMoney(int value)
    {
        if (Data.Gold >= value)
        {
            UG.Spawn(value);
            Data.Gold -= value;
            return true;
        }
        else
        {
            Wanning(Wannings.Gold);
            return false;
        }
    }
    public void UnitAdd(int n)
    {
        Data.Units.Add(n);
        Data.LocalData.GetUnits.Add((UnitClass)n, new LocalUnit());
    }
}
