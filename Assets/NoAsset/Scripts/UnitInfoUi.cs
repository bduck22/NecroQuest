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
        else {
            LoadInfo();
        }
    }
    void LoadInfo()
    {
        for(int i = 0; i < 5; i++)
        {
            if (PlayerManager.instance.Units.Length > i)
            {
                Unit unit = PlayerManager.instance.Units[i];
                Transform Cha = transform.GetChild(i);
                for (int j = 0; j < 4; j++)
                {
                    Cha.GetChild(j).gameObject.SetActive(true);
                }
                Cha.GetChild(0).GetChild(1).GetComponent<Text>().text = (unit.SkillCoolTime - unit.SkillTime).ToString("#,###");
                Cha.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(unit.UnitClass.ToString() + "Head");
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
