using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Wannings
{
    Gold,
    Unit,
    Saved,
    MaxLv,
    EmptyPre
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
    public Text DiffiT;

    public Image[] PreButton;

    string path;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Data.LocalData.Gold += 10000;
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
            case Wannings.EmptyPre:
                WText = "해당편성은 비어있습니다.";
                if (WanningT.text == WText)
                {
                    iswanning = true;
                }
                else WanningT.text = WText;
                StartCoroutine(FadeOut(0));
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
            Data.LocalData = new LocalData();
            Data.LocalData.Gold = 0;
            Data.LocalData.diffi = 0;
            SetPreset(0);
            Data.Stats = new UnitStats();
            Data.Units = new List<int>();
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
        if (Data.LocalData.Gold >= value)
        {
            UG.Spawn(value);
            Data.LocalData.Gold -= value;
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
    public void Diffi(bool b)
    {
        if (b)
        {
            Data.LocalData.diffi++;
        }
        else
        {
            if (Data.LocalData.diffi > 0)
            {
                Data.LocalData.diffi--;
            }
        }
        DiffiT.text = Data.LocalData.diffi.ToString("#,##0");
    }
    public void SetPreset(int n)
    {
        for (int i = 0; i < 3; i++)
        {
            PreButton[i].color = Color.white;
        }
        PreButton[n].color = Color.green;
        Data.LocalData.SelectPreSet = n;
    }
    public void startGame()
    {
        foreach (int l in Data.LocalData.Presets[Data.LocalData.SelectPreSet])
        {
            if (l != -1)
            {
                SceneManager.LoadScene(2);
                return;
            }
        }
        Wanning(Wannings.EmptyPre);
    }
}
