using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoUi : MonoBehaviour
{
    [SerializeField] private float LoadTime;

    float time;
    void Start()
    {
    }

    void Update()
    {
        if (time < LoadTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            LoadInfo();
        }
    }
    void LoadInfo()
    {
        for (int i = 0; i < 5; i++)
        {
            if (PlayerManager.instance.Units.Length > i)
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
                    Cha.GetChild(1).GetComponent<Image>().color = Color.red;
                    //Cha.GetChild(0).GetChild(0).GetComponent<Image>().sprite  스킬 아이콘
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
                    Cha.GetChild(0).GetComponentInChildren<Text>().text = (unit.SkillCoolTime - unit.SkillTime).ToString("#,###");
                    //Cha.GetChild(0).GetChild(0).GetComponent<Image>().sprite  스킬 아이콘
                    Cha.GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = 1 - unit.SkillTime / unit.SkillCoolTime;
                    Cha.GetChild(1).GetComponent<Image>().color = Color.white;
                    Cha.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(unit.UnitClass.ToString() + "Head");
                    Cha.GetChild(2).GetComponent<Slider>().value = unit.Hp / (unit.MaxHp * 3f);
                    Cha.GetChild(2).GetComponentInChildren<TMP_Text>().text = unit.Hp.ToString("#,###.#") + " / " + (unit.MaxHp * 3f).ToString("#,###.#");
                    Cha.GetChild(3).GetComponent<Slider>().value = unit.Moral / 250f;
                    Cha.GetChild(3).GetComponentInChildren<TMP_Text>().text = unit.Moral.ToString("#,###.#") + " / 250";
                }
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
        time = 0;
    }
}
