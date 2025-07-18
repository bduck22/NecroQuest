using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UnitStats
{
    public float Speed; //이동속도 +
    public float AttackSpeed; //공격속도 +
    public float Hp; //체력 +
    public float Damage; //공격력 +
    public float GetDamage; //받는 피해량 - .
    public float SetValue; //가하는 피해와 회복량 + .
    public float GetHeal; //받는 회복량 + .
    public float Intersection; //사거리 +
    public float AttackDamage; //기본공격피해량 + .
    public float SkillDamage; //스킬피해량 + .
    public float SkillCool; //쿨타임 +
    public float InvinTime; //무적시간 +
    public float MoralUp;
    public void PlusStat(UnitStats stats)
    {
        Speed += stats.Speed;
        AttackSpeed += stats.AttackSpeed;
        Hp += stats.Hp;
        Damage += stats.Damage;
        GetDamage += stats.GetDamage;
        SetValue += stats.SetValue;
        GetHeal += stats.GetHeal;
        Intersection += stats.Intersection;
        AttackDamage += stats.AttackDamage;
        SkillDamage += stats.SkillDamage;
        SkillCool += stats.SkillCool;
        InvinTime += stats.InvinTime;
        MoralUp += stats.MoralUp;
    }
}

public class Unit : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 TargetWid;
    public bool Move;

    [Header("Stats")]
    public int Level;
    public float Speed;
    public float AttackSpeed;
    public float Hp;
    public float MaxHp;
    public float Damage;
    public float Intersection;
    public float Moral;
    public List<Buff> Buff;
    public UnitStats PlusStats;

    [Header("Type")]
    public UnitClass UnitClass;
    public UnitTargetType UnitTargetType;

    [Header("Invin")]
    public bool Invin;
    public float InvinTime;

    [Header("Etc")]
    public Transform TargetUnit;

    [SerializeField] private float AttackTime;
    public Animator AttackAnimation;

    public Transform AttackEffect;
    [SerializeField] CircleCollider2D Interaction;

    public bool locked;
    public bool Hlocked;

    public float AttackWeight;

    public Rigidbody2D rigidbody;

    AudioSource audioSource;

    [Header("Skill")]
    public Transform SkillEffect;
    public float SkillCoolTime;
    public float SkillTime;
    public bool skill;
    public float SkillWeight;

    public float GetDamages;
    public float SetDamages;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    public void UnitInit()
    {
        AttackTime = 1;
        SkillTime = SkillCoolTime+PlusStats.SkillCool;
        Interaction.radius = Intersection + 2f+PlusStats.Intersection;
        if(Interaction.radius < 2)
        {
            Interaction.radius = 2;
        }
        TargetUnit = null;
        Invin = false;
        Move = false;
        if(GameManager.instance.GameStatus == GameStatus.Waving)locked = false;
        HpChange(-(MaxHp + PlusStats.Hp) * 2);
    }
    public void Spawn()
    {
        Buff.Clear();
        PlusStats = Data.Stats;
        Damage = Data.UnitData[UnitClass].Damage+ Data.LocalData.GetUnits[UnitClass].Damage;
        AttackSpeed = Data.UnitData[UnitClass].AttackSpeed+ Data.LocalData.GetUnits[UnitClass].AttackSpeed;
        MaxHp = Data.UnitData[UnitClass].Hp+ Data.LocalData.GetUnits[UnitClass].Hp;
        Speed = Data.UnitData[UnitClass].Speed+ Data.LocalData.GetUnits[UnitClass].Speed;
        SkillCoolTime = Data.UnitData[UnitClass].Cooltime;
        Level = Data.LocalData.GetUnits[UnitClass].level;
        switch (UnitClass)
        {
            case UnitClass.GuardN:
                PlusStats.GetDamage -= 0.15f;
                break;
            case UnitClass.Berserker:
                Buff.Add(new Buff(Buff_Type.BerserkP, Damage, AttackSpeed, 0, true));
                break;
            case UnitClass.ArchM:
                Buff.Add(new Buff(Buff_Type.Charge, 0, 0, false));
                break;
        }
        gameObject.SetActive(true);
        Hp = (MaxHp + PlusStats.Hp) * 20;
        UnitInit();
    }
    public void HpUp(float value)
    {
        PlusStats.Hp += value;
        Hp += value*20;
    }
    public void InteractionUp(float value)
    {
        PlusStats.Intersection += value;
        Interaction.radius = Intersection + 2f + PlusStats.Intersection;
        if (Interaction.radius < 2)
        {
            Interaction.radius = 2;
        }
    }
    public void AllStatUp(float value)
    {
        HpUp(value);
        PlusStats.Damage += value;
        PlusStats.Speed += value;
        PlusStats.AttackSpeed += value;
    }
    void Update()
    {

        if (TargetUnit)
        {
            if (!TargetUnit.gameObject.activeSelf)
            {
                TargetUnit = null;
            }
        }

        if (Hp <= 0)
        {
            if (PlayerManager.instance.SpawnManager.Boss)
            {
                if (PlayerManager.instance.SpawnManager.Boss.Type == MobType.Dullahan)
                {
                    PlayerManager.instance.SpawnManager.Boss.DullahanHeal(transform);
                }
            }
            Data.LocalData.GetUnits.Remove(UnitClass);
            Data.Units.Remove((int)UnitClass);
            int l = 0;
            foreach(int i in Data.LocalData.Presets[Data.LocalData.SelectPreSet])
            {
                if(i== (int)UnitClass)
                {
                    Data.LocalData.Presets[Data.LocalData.SelectPreSet][l] = -1;
                    break;
                }
                l++;
            }
            if(Data.LocalData.StartingUnit == UnitClass)
            {
                LobbyManager.Instance.UnitAdd((int)UnitClass);
            }
            PlayerManager.instance.UnitsMoral(-30);
            gameObject.SetActive(false);
            PlayerManager.instance.isAlive();
        }

        if (AttackTime < 1)
        {
            if (!locked||!Hlocked)
            {
                if ((AttackSpeed + PlusStats.AttackSpeed) <= 0)
                {
                    AttackTime += 0.1f * Time.deltaTime;
                }
                else AttackTime += (AttackSpeed+PlusStats.AttackSpeed) * Time.deltaTime;
            }
        }
        else
        {
            AttackTime = 1;
        }

        if (SkillTime < ((SkillCoolTime+ PlusStats.SkillCool)<5f?5: (SkillCoolTime + PlusStats.SkillCool)))
        {
            if (!locked|| !Hlocked)
            {
                SkillTime += Time.deltaTime;
            }
        }
        else if (!skill|| !Hlocked)
        {
            skill = true;
            SkillTime = ((SkillCoolTime + PlusStats.SkillCool) < 5f ? 5 : (SkillCoolTime + PlusStats.SkillCool));
        }

        if (TargetUnit && AttackTime == 1 && !locked&& !Hlocked)
        {
            AttackTime = 0;
            Attack();
        }

        if (Move&& !Hlocked)
        {
            if ((Vector2)transform.position != TargetWid)
            {
                if (AttackTime == 1)
                {
                    AttackAnimation.transform.localRotation = Quaternion.identity;
                    if (TargetWid.x >= transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                rigidbody.linearVelocity = ((Vector3)TargetWid - transform.position).normalized * 1.5f * ((Speed+ PlusStats.Speed)<0.5f?0.5f: (Speed + PlusStats.Speed));
                //transform.position = Vector2.MoveTowards(transform.position, TargetWid, Speed * Time.deltaTime);
            }
            if ((TargetWid-(Vector2)transform.position).sqrMagnitude <0.2f)
            {
                Move = false;
            }
        }
        else
        {
            rigidbody.linearVelocity = Vector2.zero;
        }
    }
    void Attack()
    {
        float attackweight = AttackWeight+PlusStats.SetValue + PlusStats.AttackDamage;
        AttackAnimation.SetFloat("AttackSpeed", (AttackSpeed+ PlusStats.AttackSpeed));
        if (TargetUnit.transform.position.x >= transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            AttackAnimation.transform.localRotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector2.right, TargetUnit.transform.position - transform.position).eulerAngles.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            AttackAnimation.transform.localRotation = Quaternion.Euler(0, 0, -Quaternion.FromToRotation(Vector2.left, TargetUnit.transform.position - transform.position).eulerAngles.z);
        }

        GameObject Effect = null;
        switch (UnitClass)
        {
            case UnitClass.GuardN:
                Effect = Instantiate(AttackEffect.gameObject, AttackAnimation.transform);
                break;
            case UnitClass.DragonN:
                Effect = AttackAnimation.gameObject;
                break;
            case UnitClass.Berserker:
                Effect = AttackAnimation.gameObject;
                break;
            case UnitClass.Archer:
                float R = Random.Range(0f, 1f);
                if(R < 0.2f)
                {
                    attackweight += 0.5f;
                }
                Effect = Instantiate(AttackEffect.gameObject, (AttackAnimation.transform.position + TargetUnit.position) / 2, AttackAnimation.transform.rotation);
                Effect.transform.localScale = new Vector3(Vector2.Distance(TargetUnit.position, AttackAnimation.transform.position) / 5f, 1.25f, 0.5f);
                Effect.transform.GetChild(0).localScale = new Vector3(Vector2.Distance(TargetUnit.position, AttackAnimation.transform.position) / 5f, 1.25f, 0.5f);
                break;
            case UnitClass.ArchM:
                Buff[0].Value++;
                Effect = Instantiate(AttackEffect.gameObject, TargetUnit.transform.position, AttackAnimation.transform.localRotation);
                break;
            case UnitClass.SpiritM:
                for (int i = 0; i < 3; i++)
                {
                    Effect = Instantiate(AttackEffect.gameObject, AttackAnimation.transform.position, AttackAnimation.transform.localRotation);
                    Effect.GetComponentInChildren<SpiritMove>().Target = TargetUnit.transform;
                    Effect.GetComponentInChildren<AttackEffect>().Unit = this;
                    Effect.GetComponentInChildren<AttackEffect>().Damage = Damage+PlusStats.Damage;
                    Effect.GetComponentInChildren<AttackEffect>().Weight = attackweight;
                }
                break;
            case UnitClass.HolyM:
                TargetUnit.GetComponent<Unit>().HpChange(-Damage-PlusStats.Damage);
                break;
        }
        if (UnitClass != UnitClass.HolyM)
        {
            Effect.GetComponentInChildren<AttackEffect>().Unit = this;
            Effect.GetComponentInChildren<AttackEffect>().Damage = Damage + PlusStats.Damage;
            Effect.GetComponentInChildren<AttackEffect>().Weight = attackweight;
        }

        AttackAnimation.SetTrigger("Attack");
    }
    public void Skill()
    {
        if (skill&&GameManager.instance.GameStatus == GameStatus.Waving)
        {
            float skillweight = SkillWeight + PlusStats.SetValue + PlusStats.SkillDamage;
            bool IsDamaged = false;
            skill = false;
            float Value = 0;
            SkillTime = 0;
            GameObject Effect = null;
            switch (UnitClass)
            {
                case UnitClass.GuardN:
                    Value = 10 + MaxHp * 0.75f;
                    IsDamaged = true;
                    locked = true;
                    AttackAnimation.SetTrigger("Skill");
                    Effect = Instantiate(SkillEffect.gameObject, transform.position, transform.rotation);
                    break;
                case UnitClass.DragonN:
                    IsDamaged= true;
                    Hlocked = true;
                    AttackAnimation.SetTrigger("Skill");
                    Value = 30 + AttackSpeed + PlusStats.AttackSpeed;
                    Effect = Instantiate(SkillEffect.gameObject, transform.position, AttackAnimation.transform.rotation);
                    break;
                case UnitClass.Berserker:
                    locked = true;
                    AttackAnimation.SetTrigger("Skill");
                    Buff.Add(new Buff(Buff_Type.Berserk, Damage+PlusStats.Damage, 6f, false));
                    break;
                case UnitClass.Archer:
                    IsDamaged = true;
                    locked = true;
                    AttackAnimation.SetTrigger("Skill");
                    Value = 20 + (Damage+PlusStats.Damage)/2 + (Speed+PlusStats.Speed)*0.75f;
                    Effect = Instantiate(SkillEffect.gameObject, AttackAnimation.transform.position, AttackAnimation.transform.rotation);
                    break;
                case UnitClass.ArchM:
                    IsDamaged = true;
                    locked = true;
                    if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x >= transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        AttackAnimation.transform.localRotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector2.right, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).eulerAngles.z);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        AttackAnimation.transform.localRotation = Quaternion.Euler(0, 0, -Quaternion.FromToRotation(Vector2.left, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).eulerAngles.z);
                    }
                    AttackAnimation.SetTrigger("Skill");
                    Effect = Instantiate(SkillEffect.gameObject);
                    Effect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Effect.transform.position = new Vector3(Effect.transform.position.x, Effect.transform.position.y, 0);
                    Effect.transform.localScale = new Vector3((Buff[0].Value * 0.02f) + 1f, (Buff[0].Value * 0.02f) + 1f, 1);
                    Effect = Effect.transform.GetChild(0).gameObject;
                    Value = 10 + (Damage + PlusStats.Damage) + Buff[0].Value * 0.05f;
                    break;
                case UnitClass.SpiritM:
                    locked = true;
                    AttackAnimation.SetTrigger("Skill");
                    Value = Speed + PlusStats.Speed;
                    Unit Skill_Target=null;
                    foreach(Unit t in PlayerManager.instance.Units)
                    {
                        if(t != null) {
                            if (t.gameObject.activeSelf)
                            {
                                if (!Skill_Target)
                                {
                                    if (Vector2.Distance(transform.position, t.transform.position) < ((Intersection + PlusStats.Intersection) * 0.6f) + 1.2f)
                                    {
                                        Skill_Target = t;
                                    }
                                }
                                else
                                {
                                    if (Skill_Target.Hp > t.Hp && Vector2.Distance(transform.position, t.transform.position) < ((Intersection + PlusStats.Intersection) * 0.6f) + 1.2f)
                                    {
                                        Skill_Target = t;
                                    }
                                }
                            }
                        }
                    }
                    if (!Skill_Target)
                    {
                        Skill_Target = this;
                    }
                    AttackAnimation.SetTrigger("Skill");
                    Skill_Target.HpChange(-(20 + skillweight * Value));
                    Skill_Target.Buff.Add(new Buff(Buff_Type.Spirit, (Skill_Target.Speed + Skill_Target.PlusStats.Speed)*0.35f, 3, false));
                    break;
                case UnitClass.HolyM:
                    locked = true;
                    Value = 30 + (Damage + PlusStats.Damage) * 0.25f + (Speed + PlusStats.Speed);
                    AttackAnimation.SetTrigger("Skill");
                    Effect = Instantiate(SkillEffect.gameObject, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                    Effect.transform.position = new Vector3(Effect.transform.position.x, Effect.transform.position.y, 0);
                    Collider2D[] cols = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 2.8f);
                    foreach (Collider2D col in cols)
                    {
                        if (col.transform.CompareTag("HitBox"))
                        {
                            col.transform.parent.GetComponent<Unit>().HpChange(-Value);
                        }
                    }
                    break;
            }
            if (IsDamaged)
            {
                Effect.transform.GetComponentInChildren<AttackEffect>().Unit = this;
                Effect.transform.GetComponentInChildren<AttackEffect>().Damage = Value;
                Effect.transform.GetComponentInChildren<AttackEffect>().Weight = skillweight;
                Effect.transform.GetComponentInChildren<AttackEffect>().Skill = true;
            }
        }
        else
        {
            //Debug.Log("스킬 쿨타임중");
        }
    }

    public void HpChange(float Damage)
    {
        float weight=1;
        if (Damage > 0)
        {
            weight += PlusStats.GetDamage;
            Moral -= 1;
            GetDamages += Damage * weight;
            PlayerManager.instance.Deal(transform, Damage*weight);
        }
        else
        {
            weight += PlusStats.GetHeal;
            audioSource.Play();
            PlayerManager.instance.Heal(transform, -Damage * weight);
        }
        Hp -= Damage * weight;
        if (Hp > (MaxHp+PlusStats.Hp) * 20) Hp = (MaxHp + PlusStats.Hp) *20;
    }
}
