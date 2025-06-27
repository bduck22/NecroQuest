using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoUi : MonoBehaviour
{
    [SerializeField] private float LoadTime;

    float time;

    void Update()
    {
        if (time < LoadTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
            LoadInfo();
        }
    }
    public void LoadFirst()
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerManager.instance.Units[i]!=null)
            {
                Unit unit = PlayerManager.instance.Units[i];

                Transform Cha = transform.GetChild(i);
                Cha.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(unit.UnitClass.ToString() + "S");
                Cha.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(unit.UnitClass.ToString() + "Head");
            }
            else
            {
                Transform Cha = transform.GetChild(i);
                for (int j = 0; j < 4; j++)
                {
                    Cha.GetChild(j).gameObject.SetActive(false);
                }
            }
        }
    }
    void LoadInfo()
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerManager.instance.Units[i] != null)
            {
                Unit unit = PlayerManager.instance.Units[i];

                if (unit.Hp <= 0)
                {
                    Transform Cha = transform.GetChild(i);
                    for (int j = 0; j < 4; j++)
                    {
                        Cha.GetChild(j).gameObject.SetActive(true);
                    }
                    Cha.GetChild(0).GetComponentInChildren<Text>().text = "";
                    Cha.GetChild(1).GetChild(1).GetComponent<Image>().color = Color.red;
                    Cha.GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = 0;
                    Cha.GetChild(2).GetComponent<Slider>().value = 0;
                    Cha.GetChild(2).GetComponentInChildren<TMP_Text>().text = "";
                    Cha.GetChild(3).GetComponent<Slider>().value = 0;
                    Cha.GetChild(3).GetComponentInChildren<TMP_Text>().text = "";
                }
                else
                {
                    Transform Cha = transform.GetChild(i);
                    for (int j = 0; j < 4; j++)
                    {
                        Cha.GetChild(j).gameObject.SetActive(true);
                    }
                    Cha.GetChild(0).GetComponentInChildren<Text>().text = (unit.SkillCoolTime - unit.PlusStats.SkillCool - unit.SkillTime).ToString("#,###");
                    Cha.GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = 1 - unit.SkillTime / (unit.SkillCoolTime - unit.PlusStats.SkillCool);
                    if (PlayerManager.instance.SeletedUnits.Contains((int)unit.UnitClass))
                    {
                        Cha.GetChild(1).GetComponent<Image>().color = Color.red;
                    }
                    else
                    {
                        Cha.GetChild(1).GetComponent<Image>().color = Color.white;
                    }
                    Cha.GetChild(1).GetChild(1).GetComponent<Image>().color = Color.white;
                    Cha.GetChild(2).GetComponent<Slider>().value = unit.Hp / ((unit.MaxHp + unit.PlusStats.Hp) * 20f);
                    Cha.GetChild(2).GetComponentInChildren<TMP_Text>().text = unit.Hp.ToString("#,###.#") + " / " + ((unit.MaxHp + unit.PlusStats.Hp) * 20f).ToString("#,###.#");
                    Cha.GetChild(3).GetComponent<Slider>().value = unit.Moral / 250f;
                    Cha.GetChild(3).GetComponentInChildren<TMP_Text>().text = unit.Moral.ToString("#,###.#") + " / 250";
                }
            }
        }
    }
}
