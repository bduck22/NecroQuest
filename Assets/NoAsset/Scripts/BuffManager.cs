using System.Collections;
using UnityEngine;

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
    }
    void BuffLoad()
    {
        foreach (Buff buff in (!IsUnit ? Mob.Buff : Unit.Buff))
        {
            if (buff.Run)
            {
                if (buff.Time > 0)
                {
                    buff.Run = false;
                }
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
                Unit.Speed += BT.Value;
                BuffEffect = Instantiate(GameManager.instance.BuffEffects[1].gameObject, (!IsUnit ? Mob.transform : Unit.transform.GetChild(3)));
                BuffEffect.transform.localPosition = Vector3.zero;
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
                    Unit.Speed -= BT.Value;
                    break;
            }
            (!IsUnit ? Mob.Buff : Unit.Buff).Remove(BT);
        }
    }
}
