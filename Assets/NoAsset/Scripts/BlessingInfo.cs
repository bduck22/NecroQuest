using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlessingInfo : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "공격력 +" + (Data.LocalData.Blessing[BlessingType.Attack] * 0.25f).ToString("#,##0.0") +
                    "\n공격속도 +" + (Data.LocalData.Blessing[BlessingType.Attack] * 0.1f).ToString("#,##0.0") +
                    "\n체력 +" + (Data.LocalData.Blessing[BlessingType.Defence] * 0.25f).ToString("#,##0.0") +
                    "\n받는피해량 -" + (Data.LocalData.Blessing[BlessingType.Defence] * 0.003f).ToString("#,##0.#%") +
                    "\n스킬 쿨타임 -" + (Data.LocalData.Blessing[BlessingType.Skill] * 0.05f).ToString("#,##0.0초") +
                    "\n스킬피해량 +" + (Data.LocalData.Blessing[BlessingType.Skill] * 0.04f).ToString("#,##0%") +
                    "\n획득 사기량 +" + (Data.LocalData.Blessing[BlessingType.Moral] * 0.1f).ToString("#,##0%");
    }
    public void onoff()
    {
        transform.parent.gameObject.SetActive(!transform.parent.gameObject.activeSelf);
    }
}
