using DamageNumbersPro;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //const string googlesheeturl = "https://docs.google.com/spreadsheets/d/12jlQL9fBaJSoOqOuuXTiVtZicH-X6jMGV56IdItUOHU/export?format=tsv&range=A2:G";

    //string sheetData;

    public static PlayerManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Cursor.lockState = CursorLockMode.Confined;
    }
    public Unit[] Units;
    public Unit SeletedUnit;

    public List<int> SeletedUnits;

    public Unit SelectSkill;

    public UnitManager UnitManager;

    public DamageNumberMesh HitPrefab;
    public DamageNumberMesh HealPrefab;
    public Transform HealEffect;
    public Transform HitEffect;

    public float MoralDownPer;

    //IEnumerator Start()  나중에 쓸만함
    //{
    //    if (Application.internetReachability == NetworkReachability.NotReachable)
    //    {
    //        Debug.Log("인터넷 연결에 연결되지 않았습니다.");
    //    }
    //    else
    //    {
    //        Debug.Log("인터넷 연결에 연결되어 있습니다.");
    //        using(UnityWebRequest www = UnityWebRequest.Get(googlesheeturl))
    //        {
    //            yield return www.SendWebRequest();

    //            if(www.isDone)
    //            {
    //                sheetData = www.downloadHandler.text;
    //            }
    //        }
    //    }
    //    Debug.Log(sheetData.Split('\n')[0].Split('\t')[0]);
    //}
    private void Update()
    {
        for (int i = 0; i < Units.Length; i++)
        {
            if (Units[i].gameObject.activeSelf)
            {
                if (Input.GetKeyDown((KeyCode)49 + i))
                {
                    switch (Units[i].UnitClass)
                    {
                        case UnitClass.ArchM:
                            SelectSkill = Units[i];
                            break;
                        case UnitClass.HolyM:
                            SelectSkill = Units[i];
                            break;
                        default:
                            Units[i].Skill();
                            break;
                    }
                }
                Units[i].Moral -= MoralDownPer * ((GameManager.instance.Diffi) / 2f) * Time.deltaTime;
            }
        }
    }

    public void Heal(Transform transform, float Damage)
    {
        HealPrefab.Spawn(transform.position, Damage);
        Instantiate(HealEffect, transform.position, Quaternion.identity);
    }

    public void Deal(Transform transform, float Damage)
    {
        HitPrefab.Spawn(transform.position, Damage);
        Instantiate(HitEffect, transform.position, Quaternion.identity);
    }
}
