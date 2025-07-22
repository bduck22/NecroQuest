using UnityEngine;
using UnityEngine.UI;

public class BlessingInfo : MonoBehaviour
{
    Text text;
    private void OnEnable()
    {
        UnitStats stat = PlayerManager.instance.PStat;

        text = GetComponent<Text>();
        text.text = "공격력 + " + (stat.Damage).ToString("#,##0.0") +
                    "\n공격속도 + " + (stat.AttackSpeed).ToString("#,##0.0") +
                    "\n체력 + " + (stat.Hp).ToString("#,##0.0") +
                    "\n이동속도 + " + (stat.Speed).ToString("#,##0.0") +
                    "\n받는피해량 - " + (stat.GetDamage).ToString("#,##0.#%") +
                    "\n가하는 전체 위력 + " + (stat.SetValue).ToString("#,##0.#%") +
                    "\n받는 치유량 + " + (stat.GetHeal).ToString("#,##0.#%") +
                    "\n사거리 + " + (stat.Intersection).ToString("#,##0.#") +
                    "\n기본공격 위력 + " + (stat.AttackDamage).ToString("#,##0.#%") +
                    "\n스킬 위력 + " + (stat.SkillDamage).ToString("#,##0.#%") +
                    "\n스킬 쿨타임 - " + (stat.SkillCool).ToString("#,##0.0초") +
                    "\n무적 시간 + " + (stat.InvinTime).ToString("#,##0.0##초") +
                    "\n획득 사기량 + " + (stat.MoralUp).ToString("#,##0.#%");
    }
    void Start()
    {

    }
    public void onoff()
    {
        transform.parent.gameObject.SetActive(!transform.parent.gameObject.activeSelf);
    }
}
