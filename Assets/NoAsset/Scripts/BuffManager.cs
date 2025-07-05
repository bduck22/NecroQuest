using DTT.Utils.Extensions;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuffManager : MonoBehaviour
{
    Unit Unit;
    MobBase Mob;
    public bool IsUnit;
    public float LodingTime;
    private float time;
    void Start()
    {
        if (IsUnit)
        {
            Unit = GetComponent<Unit>();
        }
        else
        {
            Mob = GetComponent<MobBase>();
        }
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= LodingTime)
        {
            time = 0;
            BuffLoad();
        }
        if (IsUnit)
        {
            if (Unit.Moral > 0 && Unit.Moral <= 50)
            {
                if (Unit.Buff.FindIndex(item => item.Type.Equals(Buff_Type.Moral1)) == -1)
                {
                    Unit.Buff.Add(new Buff(Buff_Type.Moral1, 0, 0, false));
                    Unit.PlusStats.GetDamage += 0.1f;
                    Unit.PlusStats.SetValue -= 0.1f;
                    Unit.PlusStats.GetHeal -= 0.1f;
                }
            }
            else
            {
                if (Unit.Buff.FindIndex(item => item.Type.Equals(Buff_Type.Moral1)) != -1)
                {
                    Unit.Buff.RemoveAt(Unit.Buff.FindIndex(item => item.Type.Equals(Buff_Type.Moral1)));
                    Unit.PlusStats.GetDamage -= 0.1f;
                    Unit.PlusStats.SetValue += 0.1f;
                    Unit.PlusStats.GetHeal += 0.1f;
                }
            }

            if (Unit.Moral >50 && Unit.Moral <= 100)
            {
                if (Unit.Buff.FindIndex(item => item.Type.Equals(Buff_Type.Moral2))==-1)
                {
                    Unit.Buff.Add(new Buff(Buff_Type.Moral2, 0, 0, false));
                    Unit.InteractionUp(-1);
                }
            }
            else
            {
                if (Unit.Buff.FindIndex(item => item.Type.Equals(Buff_Type.Moral2)) != -1)
                {
                    Unit.Buff.RemoveAt(Unit.Buff.FindIndex(item => item.Type.Equals(Buff_Type.Moral2)));
                    Unit.InteractionUp(1);
                }
            }

            if (Unit.Moral > 150 && Unit.Moral <= 200)
            {
                if (Unit.Buff.FindIndex(item => item.Type.Equals(Buff_Type.Moral4)) == -1)
                {
                    Unit.Buff.Add(new Buff(Buff_Type.Moral4, 0, 0, false));
                    Unit.AllStatUp(0.5f);

                }
            }
            else
            {
                if (Unit.Buff.FindIndex(item => item.Type.Equals(Buff_Type.Moral4)) != -1)
                {
                    Unit.Buff.RemoveAt(Unit.Buff.FindIndex(item => item.Type.Equals(Buff_Type.Moral4)));
                    Unit.AllStatUp(-0.5f);
                }
            }

            if (Unit.Moral > 200 && Unit.Moral <= 250)
            {
                if (Unit.Buff.FindIndex(item => item.Type.Equals(Buff_Type.Moral5)) == -1)
                {
                    Unit.Buff.Add(new Buff(Buff_Type.Moral5, 0, 0, false));
                    Unit.PlusStats.GetDamage -= 0.1f;
                    Unit.PlusStats.SetValue += 0.1f;
                    Unit.PlusStats.GetHeal += 0.1f;
                }
            }
            else
            {
                if (Unit.Buff.FindIndex(item => item.Type.Equals(Buff_Type.Moral5)) != -1)
                {
                    Unit.Buff.RemoveAt(Unit.Buff.FindIndex(item => item.Type.Equals(Buff_Type.Moral5)));
                    Unit.PlusStats.GetDamage += 0.1f;
                    Unit.PlusStats.SetValue -= 0.1f;
                    Unit.PlusStats.GetHeal -= 0.1f;
                }
            }
        }
    }
    void BuffLoad()
    {
        foreach (Buff buff in (!IsUnit ? Mob.Buff : Unit.Buff))
        {
            if (buff.Run||buff.Loop)
            {
                buff.Run = false;
                StartCoroutine(Buff(buff));
            }
        }
    }
    IEnumerator Buff(Buff BT)
    {
        GameObject BuffEffect = null;
        switch (BT.Type)
        {
            case Buff_Type.Provo:
                if (IsUnit) {
                    Unit.TargetUnit = BT.Target;
                }
                else
                {
                    Mob.Target = BT.Target.GetComponent<Unit>();
                }
                BuffEffect = Instantiate(GameManager.instance.BuffEffects[0].gameObject, (!IsUnit? Mob.transform.GetChild(0) : Unit.transform.GetChild(5)));
                break;
            case Buff_Type.Spirit:
                Unit.PlusStats.Speed += BT.Value;
                BuffEffect = Instantiate(GameManager.instance.BuffEffects[1].gameObject, (!IsUnit ? Mob.transform : Unit.transform.GetChild(3)));
                BuffEffect.transform.localPosition = Vector3.zero;
                break;
            case Buff_Type.Berserk:
                BuffEffect = Instantiate(GameManager.instance.BuffEffects[2].gameObject, (!IsUnit ? Mob.transform : Unit.transform));
                Unit.PlusStats.Speed += 1.5f;
                Unit.PlusStats.AttackDamage += BT.Value*0.05f;
                Unit.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                Unit.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().size += new Vector2(1.5f,2.3f);
                Unit.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().offset += new Vector2(-0.75f, 1.15f);
                Unit.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).localScale += new Vector3(2.5f, 2.5f, 2.5f);
                break;
            case Buff_Type.BerserkP:
                float lostHP = ((Unit.PlusStats.Hp + Unit.MaxHp) * 20 - Unit.Hp);
                Unit.Damage = BT.Value + lostHP / 20f * 0.75f;
                Unit.AttackSpeed = BT.Value2 + lostHP / 30f * 0.25f;
                break;
        }
        if(BT.Time <= 0)
        {
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(BT.Time);
            switch (BT.Type)
            {
                case Buff_Type.Provo:
                    BuffEffect.GetComponent<Animator>().enabled = true;
                    if (IsUnit)
                    {
                        Unit.TargetUnit = null;
                    }
                    else
                    {
                        Mob.Target = null;
                    }
                    break;
                case Buff_Type.Spirit:
                    BuffEffect.GetComponent<Animator>().enabled = true;
                    Unit.PlusStats.Speed -= BT.Value;
                    break;
                case Buff_Type.Berserk:
                    BuffEffect.GetComponent<ParticleSystem>().loop = false;
                    Unit.PlusStats.Speed -= 1.5f;
                    Unit.PlusStats.AttackDamage -= BT.Value * 0.05f;
                    Unit.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                    Unit.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().size -= new Vector2(1.5f, 2.3f);
                    Unit.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().offset -= new Vector2(-0.75f, 1.15f);
                    Unit.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).localScale -= new Vector3(2.5f, 2.5f, 2.5f);
                    break;
            }
            (!IsUnit ? Mob.Buff : Unit.Buff).Remove(BT);
        }
    }
}
