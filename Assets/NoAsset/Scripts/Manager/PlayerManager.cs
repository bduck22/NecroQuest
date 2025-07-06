using DamageNumbersPro;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum QuestType
{
    Wave,
    Monster,
    Attack
}

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;
    private void Awake()
    {
        UnitManager = GetComponent<UnitManager>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Cursor.lockState = CursorLockMode.Confined;
        Units = new Unit[4];
    }
    public Unit[] Units;

    public List<int> SeletedUnits;

    public Unit SelectSkill;

    public UnitManager UnitManager;

    public DamageNumberMesh HitPrefab;
    public DamageNumberMesh HealPrefab;
    public Transform HealEffect;
    public Transform HitEffect;

    public float MoralDownPer;

    public List<GoldBase> Goldpool;

    public Transform GoldOb;

    public List<Guardian> guardians;

    public Transform InfoPop;

    public SpawnManager SpawnManager;

    public Transform UnitSpawnPoints;

    public Transform[] UnitPrefabs;

    public QuestType QuestType;
    public float QuestValue;
    public float OQuestValue;

    public Transform QuestClear;

    public UnitInfoUi UUi;

    public TMP_Text QuestT;

    public int killcount;
    public void CreateGold(int value, Vector2 position)
    {
        GoldBase gold = null;
        foreach (GoldBase g in Goldpool)
        {
            if (!g.gameObject.activeSelf && !gold)
            {
                g.transform.position = position;
                g.gameObject.SetActive(true);
                gold = g;
                break;
            }
        }
        if (!gold)
        {
            gold = Instantiate(GoldOb, position, Quaternion.identity).GetComponent<GoldBase>();
            Goldpool.Add(gold);
        }
        gold.Value = value;
    }

    public void UnitsMoral(float Moral)
    {
        foreach (Unit unit in Units)
        {
            if (unit != null)
            {
                unit.Moral += Moral * (1 + unit.PlusStats.MoralUp);
                if (unit.Moral <= 0) unit.Moral = 0;
                else if (unit.Moral > 250) unit.Moral = 250;
            }
        }
    }

    string Qdesc = string.Empty;
    string Qdesc2 = string.Empty;

    public bool Alive=false;
    private void Update()
    {
        if (GameManager.instance.GameStatus != GameStatus.Waving)
        {
            if (QuestValue <= 0)
            {
                QuestClear.gameObject.SetActive(true);
            }
            return;
        }
        else
        {
            if(QuestClear.gameObject.activeSelf) QuestClear.gameObject.SetActive(false);
        }
        QuestT.text = OQuestValue.ToString("#,##0") + Qdesc+"\n(" + (OQuestValue - QuestValue).ToString("#,##0") + Qdesc2+")";

        for (int i = 0; i < Units.Length; i++)
        {
            if (Units[i] != null && Units[i].gameObject.activeSelf)
            {
                if (Input.GetKeyDown((KeyCode)49 + i))
                {
                    SkillUse(i);
                }
                Units[i].Moral -= MoralDownPer * ((GameManager.instance.Diffi+1) / 2f) * Time.deltaTime;
                if (Units[i].Moral <= 0) Units[i].Moral = 0;
                else if (Units[i].Moral > 250) Units[i].Moral = 250;
            }
        }
    }
    public void isAlive()
    {
        for(int i = 0; i < Units.Length; i++)
        {
            if (Units[i] != null && Units[i].gameObject.activeSelf)
            {
                Alive = true;
            }
        }
        if (!Alive)
        {
            GameManager.instance.GameStatus = GameStatus.Result;
        }
        if(GameManager.instance.GameStatus != GameStatus.Result) Alive = false;
    }
    public void Return()
    {
        GameManager.instance.GameStatus = GameStatus.Result;
        isAlive();
    }
    public void StageStart()
    {
        GameManager.instance.Diffi = Data.LocalData.diffi;
        QuestType = (QuestType)Random.Range(0, 3);
        QuestValue = Random.Range(1, 10);
        switch (QuestType)
        {
            case QuestType.Attack:
                Qdesc = "의 피해 입히기";
                Qdesc2 = "의 피해 입힘";
                QuestValue = (GameManager.instance.Diffi == 0 ? 200 : GameManager.instance.Diffi * 500);
                break;
            case QuestType.Wave:
                Qdesc = "웨이브 클리어하기";
                Qdesc2 = "웨이브 클리어";
                QuestValue = GameManager.instance.Diffi + 2;
                break;
            case QuestType.Monster:
                Qdesc = "마리의 몬스터 처치하기";
                Qdesc2 = "마리의 몬스터 처치함";
                QuestValue = (GameManager.instance.Diffi == 0 ? 1 : GameManager.instance.Diffi) * 50 * (1+ ((int)(GameManager.instance.Diffi / 5)*0.5f)) ;
                //QuestValue = ;
                break;
        }
        OQuestValue = QuestValue;
        for (int i = 0; i < 4; i++)
        {
            if (Data.LocalData.Presets[Data.LocalData.SelectPreSet][i] != -1)
            {
                Unit Unit = Instantiate(UnitPrefabs[Data.LocalData.Presets[Data.LocalData.SelectPreSet][i]].gameObject, UnitSpawnPoints.GetChild(i).transform.position, Quaternion.identity).GetComponent<Unit>();
                Units[i] = Unit;
                Unit.Spawn();
            }
        }
        foreach (Unit unit in Units)
        {
            if (unit != null)
            {
                if (unit.UnitClass == UnitClass.SpiritM)
                {
                    float a = unit.Damage;
                    if (a < unit.Speed) a = unit.Speed;
                    if (a < unit.AttackSpeed) a = unit.AttackSpeed;
                    if (a < unit.MaxHp) a = unit.MaxHp;

                    foreach (Unit unit2 in Units)
                    {
                        if (unit2 != null)
                        {
                            if (a <= unit.Damage) unit2.PlusStats.Damage += unit.Damage / 5f;
                            if (a <= unit.Speed) unit2.PlusStats.Speed += unit.Speed / 5f;
                            if (a <= unit.AttackSpeed) unit2.PlusStats.AttackSpeed += unit.AttackSpeed / 5f;
                            if (a <= unit.MaxHp) unit2.PlusStats.Hp += unit.MaxHp / 5f;
                        }
                    }
                }
                unit.PlusStats.PlusStat(Data.Stats);
            }
        }
        UUi.LoadFirst();
        GameManager.instance.GameStatus = GameStatus.WaveStart;
    }
    public void SkillUse(int i)
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
    public void GuardianLoad()
    {
        foreach (Unit unit in Units)
        {
            if (unit != null)
            {
                if (unit.gameObject.activeSelf)
                {
                    unit.PlusStats.PlusStat(guardians[guardians.Count - 1].Stats);
                    unit.UnitInit();
                }
            }
        }
    }
    public void UnitsInit()
    {
        foreach (Unit unit in Units)
        {
            if (unit != null)
            {
                if (unit.gameObject.activeSelf)
                {
                    unit.UnitInit();
                }
            }
        }
    }
    public void UnitStop()
    {
        foreach (Unit unit in Units)
        {
            if (unit != null)
            {
                unit.locked = true;
            }
        }
    }
    public bool Checklock()
    {
        foreach (Unit unit in Units)
        {
            if (unit != null)
            {
                if (unit.locked || unit.Hlocked) return false;
            }
        }
        UnitStop();
        return true;
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

    public bool open = true;
    public int opened = -1;
    public void ChaInfo(int number)
    {
        if (opened == number)
        {
            open = false;
        }
        else
        {
            open = true;
        }
        if (!open) opened = -1;
        else opened = number;
        InfoPop.gameObject.SetActive(open);
        if (open) InfoPop.GetComponent<InfomationUI>().On(number);
    }
    void OnApplicationQuit()
    {
        Data.Save();
    }
}
