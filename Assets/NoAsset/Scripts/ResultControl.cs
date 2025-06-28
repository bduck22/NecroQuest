using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultControl : MonoBehaviour
{
    
    void Start()
    {
        transform.GetChild(1).GetComponent<TMP_Text>().text = "획득한 골드 : " + GameManager.instance.gold.ToString("#,##0$");
        transform.GetChild(2).GetComponent<TMP_Text>().text = "총 처치한 몬스터 수 : " + PlayerManager.instance.killcount.ToString("#,##0");
        int i = 0;
        float get=0, set=0;
        foreach(Unit unit in PlayerManager.instance.Units)
        {
            Transform u = transform.GetChild(6 + i);
            if (unit != null)
            {
                get += unit.GetDamages;
                set += unit.SetDamages;
                
                u.gameObject.SetActive(true);
                u.GetComponent<Image>().sprite = Resources.Load<Sprite>(unit.UnitClass.ToString() + "Head");
                u.GetComponent<Image>().color = (unit.gameObject.activeSelf?Color.white:Color.red);
                u.GetChild(0).GetComponent<TMP_Text>().text = Data.UnitData[unit.UnitClass].Name;
                u.GetChild(1).GetComponent<TMP_Text>().text = "상태 : " + (unit.gameObject.activeSelf?"생존":"사망");
                u.GetChild(3).GetComponent<TMP_Text>().text = "받은 피해량 : " + unit.GetDamages.ToString("#,##0");
                u.GetChild(2).GetComponent<TMP_Text>().text = "입힌 피해량 : " + unit.SetDamages.ToString("#,##0");
            }
            else
            {
                u.gameObject.SetActive(false);
            }
            i++;
        }
        transform.GetChild(3).GetComponent<TMP_Text>().text = "총 입힌 피해량 : " +  set.ToString("#,##0");
        transform.GetChild(4).GetComponent<TMP_Text>().text = "총 받은 피해량 : " + get.ToString("#,##0");
        if (GameManager.instance.Cleared)
        {
            transform.GetChild(5).GetComponent<TMP_Text>().text = "난이도 " + GameManager.instance.Diffi + " 클리어";
            transform.GetChild(0).GetComponent<TMP_Text>().text = "클리어한 웨이브 : " + (GameManager.instance.Wave+1).ToString("#,##0") + " / " + GameManager.instance.Waves.Length.ToString("#,##0");
        }
        else if (PlayerManager.instance.Alive)
        {
            transform.GetChild(5).GetComponent<TMP_Text>().text = "난이도 " + GameManager.instance.Diffi + " 귀환";
            transform.GetChild(0).GetComponent<TMP_Text>().text = "클리어한 웨이브 : " + (GameManager.instance.Wave+1).ToString("#,##0") + " / " + GameManager.instance.Waves.Length.ToString("#,##0");
        }
        else
        {
            transform.GetChild(5).GetComponent<TMP_Text>().text = "난이도 " + GameManager.instance.Diffi + " 실패";
            transform.GetChild(0).GetComponent<TMP_Text>().text = "클리어한 웨이브 : " + (GameManager.instance.Wave).ToString("#,##0") + " / " + GameManager.instance.Waves.Length.ToString("#,##0");
        }
    }
    public void Confirm()
    {
        Data.LocalData.Gold += GameManager.instance.gold;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
