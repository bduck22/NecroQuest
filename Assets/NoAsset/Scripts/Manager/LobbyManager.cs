using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public enum Wannings
{
    Gold,
    Unit,
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
        StartCoroutine(Opening());
    }
    public Text WanningT;
    public Image Open;
    public Transform Starting;
    public Text DiffiT;
    public Image[] PreButton;
    public Transform Blessings;
    public Transform BlessingLv;
    public Transform Setting;

    public AudioMixer AudioMixer;

    string path;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Data.LocalData.Gold += 10000;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Setting.gameObject.SetActive(!Setting.gameObject.activeSelf);
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
        while (WanningT.color.a > 0)
        {
            WanningT.color -= Color.black * (value);
            yield return new WaitForSeconds(0.02f);
        }
        WanningT.gameObject.SetActive(false);

        iswanning = false;
    }
    IEnumerator Opening()
    {
        if (!File.Exists(Data.path))
        {
            Starting.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        yield return new WaitForSeconds(0.5f);
        DiffiT.text = Data.LocalData.diffi.ToString("#,##0");
        for (int i = 1; i <= 4; i++)
        {
            BuffTLoad(i);
        }
        for (int i = 0; i < 3; i++)
        {
            PreButton[i].color = Color.white;
        }
        PreButton[Data.LocalData.SelectPreSet].color = Color.green;

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
            Data.Save();
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
        Data.Save();
    }
    public void Diffi(bool b)
    {
        if (b)
        {
            if (Data.LocalData.diffi < 10)
            {
                Data.LocalData.diffi++;
            }
        }
        else
        {
            if (Data.LocalData.diffi > 0)
            {
                Data.LocalData.diffi--;
            }
        }
        Data.Save();
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
        Data.Save();
    }
    public void startGame()
    {
        foreach (int l in Data.LocalData.Presets[Data.LocalData.SelectPreSet])
        {
            if (l != -1)
            {
                Data.Stats.Damage += (Data.LocalData.Blessing[BlessingType.Attack] * 0.25f);
                Data.Stats.AttackSpeed += (Data.LocalData.Blessing[BlessingType.Attack] * 0.1f);
                Data.Stats.Hp += (Data.LocalData.Blessing[BlessingType.Defence] * 0.25f);
                Data.Stats.GetDamage -= (Data.LocalData.Blessing[BlessingType.Defence] * 0.003f);
                Data.Stats.SkillCool -= (Data.LocalData.Blessing[BlessingType.Skill] * 0.05f);
                Data.Stats.SkillDamage += (Data.LocalData.Blessing[BlessingType.Skill] * 0.04f);
                Data.Stats.MoralUp += (Data.LocalData.Blessing[BlessingType.Moral] * 0.1f);
                SceneManager.LoadScene(3);
                return;
            }
        }
        Wanning(Wannings.EmptyPre);
    }
    public void BuffLevelUp(int Type)
    {
        if (!UseMoney((Data.LocalData.Blessing[(BlessingType)(Type)] + 1) * 500))
        {
            return;
        }
        Data.LocalData.Blessing[(BlessingType)(Type)]++;
        Data.Save();
        BuffTLoad(Type + 1);
    }
    public void BuffTLoad(int n)
    {
        Blessings.GetChild(n).GetChild(2).GetComponent<TMP_Text>().text = "Lv." + Data.LocalData.Blessing[(BlessingType)(n - 1)].ToString("#,##0");
        switch (n - 1)
        {
            case 0:
                Blessings.GetChild(n).GetChild(3).GetComponent<TMP_Text>().text = "공격력 +" + (Data.LocalData.Blessing[(BlessingType)(n - 1)] * 0.3f).ToString("#,##0.0") +
                    "\n공격속도 +" + (Data.LocalData.Blessing[(BlessingType)(n - 1)] * 0.1f).ToString("#,##0.0");
                break;
            case 1:
                Blessings.GetChild(n).GetChild(3).GetComponent<TMP_Text>().text = "체력 +" + (Data.LocalData.Blessing[(BlessingType)(n - 1)] * 0.25f).ToString("#,##0.0") +
                    "\n받는피해량 -" + (Data.LocalData.Blessing[(BlessingType)(n - 1)] * 0.003f).ToString("#,##0.#%");
                break;
            case 2:
                Blessings.GetChild(n).GetChild(3).GetComponent<TMP_Text>().text = "스킬 쿨타임 -" + (Data.LocalData.Blessing[(BlessingType)(n - 1)] * 0.1f).ToString("#,##0.0초") +
                    "\n스킬피해량 +" + (Data.LocalData.Blessing[(BlessingType)(n - 1)] * 0.04f).ToString("#,##0%");
                break;
            case 3:
                Blessings.GetChild(n).GetChild(3).GetComponent<TMP_Text>().text = "획득 사기량 +" + (Data.LocalData.Blessing[(BlessingType)(n - 1)] * 0.1f).ToString("#,##0%");
                break;
        }
        Blessings.GetChild(n).GetChild(4).GetComponentInChildren<TMP_Text>().text = "레벨업(" + ((Data.LocalData.Blessing[(BlessingType)(n - 1)] + 1) * 500).ToString("#,##0$)");
        BlessingLv.GetChild(n - 1).GetChild(1).GetComponent<Text>().text = "Lv." + Data.LocalData.Blessing[(BlessingType)(n - 1)].ToString("#,##0");
    }
    public void MainScreen()
    {
        SceneManager.LoadScene(0);
        //메인화며으로이동
    }
    public void Quit()
    {
        Data.Save();
        Application.Quit();
    }
}
