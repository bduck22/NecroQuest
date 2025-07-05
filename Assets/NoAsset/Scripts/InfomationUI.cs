using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfomationUI : MonoBehaviour
{
    private Image icon;
    private Text name;
    private Text level;
    private Text hp;
    private Text attackspeed;
    private Text damage;
    private Text speed;
    private Slider healthS;
    private TMP_Text healthT;
    private Slider moralS;
    private TMP_Text moralT;
    private Image P;
    private Image S;

    public Unit unit;
    public UnitClass uClass;
    private void Awake()
    {
        icon = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        name = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        level = transform.GetChild(1).GetChild(1).GetComponent<Text>();

        hp = transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>();
        damage = transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>();
        attackspeed = transform.GetChild(2).GetChild(1).GetChild(2).GetComponent<Text>();
        speed = transform.GetChild(2).GetChild(1).GetChild(3).GetComponent<Text>();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            P = transform.GetChild(3).GetChild(0).GetComponent<Image>();
            S = transform.GetChild(4).GetChild(0).GetComponent<Image>();
        }
        else
        {
            moralS = transform.GetChild(3).GetChild(0).GetComponent<Slider>();
            moralT = transform.GetChild(3).GetChild(1).GetComponent<TMP_Text>();
            healthS = transform.GetChild(4).GetChild(0).GetComponent<Slider>();
            healthT = transform.GetChild(4).GetChild(1).GetComponent<TMP_Text>();

            P = transform.GetChild(5).GetChild(0).GetComponent<Image>();
            S = transform.GetChild(6).GetChild(0).GetComponent<Image>();
        }
    }
    public void On(int n)
    {
        unit = PlayerManager.instance.Units[n];

        icon.sprite = Resources.Load<Sprite>(unit.UnitClass.ToString() + "Head");
        name.text = Data.UnitData[unit.UnitClass].Name;
        level.text = "Lv : " + unit.Level.ToString("#,##0");
        P.sprite = Resources.Load<Sprite>(unit.UnitClass.ToString() + "P");
        S.sprite = Resources.Load<Sprite>(unit.UnitClass.ToString() + "S");
    }
    public void On()
    {
        unit = new Unit();
        unit.Damage = Data.UnitData[uClass].Damage + Data.LocalData.GetUnits[uClass].Damage;
        unit.UnitClass = uClass;
        unit.Speed = Data.UnitData[uClass].Speed + Data.LocalData.GetUnits[uClass].Speed;
        unit.MaxHp = Data.UnitData[uClass].Hp + Data.LocalData.GetUnits[uClass].Hp;
        unit.AttackSpeed = Data.UnitData[uClass].AttackSpeed + Data.LocalData.GetUnits[uClass].AttackSpeed;

        icon.sprite = Resources.Load<Sprite>(uClass.ToString() + "Head");
        name.text = Data.UnitData[uClass].Name;
        level.text = "Lv : " + Data.LocalData.GetUnits[uClass].level.ToString("#,##0");
        P.sprite = Resources.Load<Sprite>(uClass.ToString() + "P");
        S.sprite = Resources.Load<Sprite>(uClass.ToString() + "S");
    }
    void Update()
    {

        hp.text = (unit.MaxHp + unit.PlusStats.Hp).ToString("#,##0.0");
        speed.text = (unit.Speed + unit.PlusStats.Speed).ToString("#,##0.0");
        damage.text = (unit.Damage + unit.PlusStats.Damage).ToString("#,##0.0");
        attackspeed.text = (unit.AttackSpeed + unit.PlusStats.AttackSpeed).ToString("#,##0.0");
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            moralS.value = unit.Moral / 250f;
            moralT.text = unit.Moral.ToString("#,##0.0") + " / " + 250f;
            healthS.value = unit.Hp / (unit.MaxHp + unit.PlusStats.Hp) * 20f;
            healthT.text = unit.Hp.ToString("#,##0.0") + " / " + ((unit.MaxHp + unit.PlusStats.Hp) * 20f).ToString("#,##0.0");
        }
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
    }
}
