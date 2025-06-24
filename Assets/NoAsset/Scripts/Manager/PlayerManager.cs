using DamageNumbersPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Pool;


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
    public void CreateGold(int value, Vector2 position)
    {
        GoldBase gold = null;
        foreach (GoldBase g in Goldpool)
        {
            if (!g.gameObject.activeSelf&&!gold)
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
        foreach(Unit unit in Units)
        {
            unit.Moral += Moral;
            if (unit.Moral <= 0) unit.Moral = 0;
            else if (unit.Moral > 250) unit.Moral = 250;
        }
    }

    private void Update()
    {
        if (GameManager.instance.GameStatus != GameStatus.Waving)
        {
            return;
        }
        for (int i = 0; i < Units.Length; i++)
        {
            if (Units[i].gameObject.activeSelf)
            {
                if (Input.GetKeyDown((KeyCode)49 + i))
                {
                    SkillUse(i);
                }
                Units[i].Moral -= MoralDownPer * ((GameManager.instance.Diffi) / 2f) * Time.deltaTime;
                if (Units[i].Moral <= 0) Units[i].Moral = 0;
                else if (Units[i].Moral > 250) Units[i].Moral = 250;
            }
        }
    }
    public void StageStart()
    {
        foreach(Unit unit in Units)
        {
            if (unit.UnitClass == UnitClass.SpiritM)
            {
                float a = unit.Damage;
                if(a<unit.Speed) a = unit.Speed;
                if(a<unit.AttackSpeed) a = unit.AttackSpeed;
                if(a<unit.MaxHp) a = unit.MaxHp;

                foreach(Unit unit2 in Units)
                {
                    if (a <= unit.Damage) unit2.PlusStats.Damage += Mathf.CeilToInt(unit.Damage / 5f / 0.5f) * 0.5f;
                    if (a <= unit.Speed) unit2.PlusStats.Speed += Mathf.CeilToInt(unit.Speed / 5f / 0.5f) * 0.5f;
                    if (a <= unit.AttackSpeed) unit2.PlusStats.AttackSpeed += Mathf.CeilToInt(unit.AttackSpeed / 5f / 0.5f) * 0.5f;
                    if (a <= unit.MaxHp) unit2.PlusStats.Hp += Mathf.CeilToInt(unit.MaxHp / 5f / 0.5f) * 0.5f;
                }
            }
        }
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
            unit.PlusStats.PlusStat(guardians[guardians.Count-1].Stats);
            unit.UnitInit();
        }
    }
    public void UnitsInit()
    {
        foreach (Unit unit in Units)
        {
            unit.UnitInit();
        }
    }
    public void UnitStop()
    {
        foreach (Unit unit in Units)
        {
            unit.locked = true;
        }
    }
    public bool Checklock()
    {
        foreach (Unit unit in Units)
        {
            if (unit.locked||unit.Hlocked) return false;
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
    public int opened=-1;
    public void ChaInfo(int number)
    {
        if (opened == number)
        {
            open = false;
        }
        else open = true;
        if(!open) opened = -1;
        else opened = number;
        InfoPop.gameObject.SetActive(open);
    } 
}
