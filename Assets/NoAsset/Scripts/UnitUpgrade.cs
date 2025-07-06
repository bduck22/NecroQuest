using MaykerStudio.Demo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitUpgrade : MonoBehaviour
{
    public UnitClass uClass;
    Text Sname;
    Text Sdesc;
    Text Pname;
    Text Pdesc;
    Text Hp;
    Text Damage;
    Text AttackSpeed;
    Text Speed;
    Image Profile;
    Image Simage;
    Image Pimage;
    Text Name;
    Text Level;

    public int Price;
    TMP_Text LevelUp;
    TMP_Text StatInit;
    Text Point;
    public int levelpoint;

    Transform LevelUpButton;
    private void Awake()
    {
        Sname = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        Sdesc = transform.GetChild(2).GetChild(1).GetComponent<Text>();
        Pname = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        Pdesc = transform.GetChild(1).GetChild(1).GetComponent<Text>();
        Hp = transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>();
        Damage = transform.GetChild(0).GetChild(2).GetChild(2).GetChild(1).GetComponent<Text>();
        AttackSpeed = transform.GetChild(0).GetChild(2).GetChild(2).GetChild(2).GetComponent<Text>();
        Speed = transform.GetChild(0).GetChild(2).GetChild(2).GetChild(3).GetComponent<Text>();
        Profile = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();

        Simage = transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>();
        Pimage = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>();
        Name = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        Level = transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();

        LevelUp = transform.GetChild(0).GetChild(5).GetComponentInChildren<TMP_Text>();
        StatInit = transform.GetChild(0).GetChild(6).GetComponentInChildren<TMP_Text>();
        Point = transform.GetChild(0).GetChild(7).GetComponent<Text>();
        LevelUpButton = transform.GetChild(0).GetChild(2).GetChild(1);
    }
    public void Set(int n)
    {
        if (uClass != (UnitClass)Data.Units[n])
        {
            uClass = (UnitClass)Data.Units[n];
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        Load();
    }
    public void Load()
    {
        LocalUnit data = Data.LocalData.GetUnits[uClass];
        levelpoint = data.level - (int)((data.Damage + data.AttackSpeed + data.Speed + data.Hp) / 0.5f);

        Sname.text = Data.UnitData[uClass].Skill.Name;
        Sdesc.text = Data.UnitData[uClass].Skill.Description;
        Pname.text = Data.UnitData[uClass].Passive.Name;
        Pdesc.text = Data.UnitData[uClass].Passive.Description;
        Hp.text = (Data.UnitData[uClass].Hp + Data.LocalData.GetUnits[uClass].Hp).ToString("#,##0.0");
        Damage.text =(Data.UnitData[uClass].Damage + Data.LocalData.GetUnits[uClass].Damage).ToString("#,##0.0");
        AttackSpeed.text = (Data.UnitData[uClass].AttackSpeed + Data.LocalData.GetUnits[uClass].AttackSpeed).ToString("#,##0.0");
        Speed.text = (Data.UnitData[uClass].Speed + Data.LocalData.GetUnits[uClass].Speed).ToString("#,##0.0");
        Profile.sprite = Resources.Load<Sprite>(uClass.ToString() + "Head");

        Simage.sprite = Resources.Load<Sprite>(uClass.ToString() + "S");
        Pimage.sprite = Resources.Load<Sprite>(uClass.ToString() + "P");
        Name.text = Data.UnitData[uClass].Name;
        if (Data.LocalData.GetUnits[uClass].level == 25)
        {
            Level.text = "Lv.Max";
            Price = 25000;
            LevelUp.text = "최대 레벨";
        }
        else
        {
            Price = (data.level + 1) * 500;
            Level.text = "Lv." + Data.LocalData.GetUnits[uClass].level;
            LevelUp.text = "+1 레벨(" + Price.ToString("#,##0") + "$)";
        }


        StatInit.text = "능력치 초기화(" + Price.ToString("#,##0") + "$)";

        Point.text = "남은 포인트 : " + levelpoint.ToString("#,##0");
        if (levelpoint > 0)
        {
            LevelUpButton.gameObject.SetActive(true);
        }
        else
        {
            LevelUpButton.gameObject.SetActive(false);
        }
    }
    public void Levelup()
    {
        if (Data.LocalData.GetUnits[uClass].level == 25)
        {
            LobbyManager.Instance.Wanning(Wannings.MaxLv);
        }
        else {
            if (LobbyManager.Instance.UseMoney(Price))
            {
                Data.LocalData.GetUnits[uClass].level++;
            }
        }
        Load();
    }
    public void Statup(int type)
    {
        if (levelpoint <= 0)
        {
            return;
        }
        switch (type)
        {
            case 0:
                levelpoint--;
                Data.LocalData.GetUnits[uClass].Hp += 0.5f;
                break;
            case 1:
                levelpoint--;
                Data.LocalData.GetUnits[uClass].Damage += 0.5f;
                break;
            case 2:
                levelpoint--;
                Data.LocalData.GetUnits[uClass].AttackSpeed += 0.5f;
                break;
            case 3:
                levelpoint--;
                Data.LocalData.GetUnits[uClass].Speed += 0.5f;
                break;
        }
        Load();
    }
    public void Statinit()
    {
        if(levelpoint == Data.LocalData.GetUnits[uClass].level)
        {
            return;
        }
        if (LobbyManager.Instance.UseMoney(Price))
        {
            Data.LocalData.GetUnits[uClass].Hp = 0;
            Data.LocalData.GetUnits[uClass].Damage = 0;
            Data.LocalData.GetUnits[uClass].AttackSpeed = 0;
            Data.LocalData.GetUnits[uClass].Speed = 0;
        }
        Load();
    }
}
