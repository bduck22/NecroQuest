using DamageNumbersPro;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{

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

    //public 

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
                if (Units[i].Moral <= 0) Units[i].Moral = 0;
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
