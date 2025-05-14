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
        }else
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

    private void Update()
    {
        for (int i = 0; i < Units.Length; i++)
        {
            if (Input.GetKeyDown((KeyCode)49+i))
            {
                if (Units[i].gameObject.activeSelf)
                {
                    switch (Units[i].UB.UnitClass)
                    {
                        case UnitClass.GuardN:
                        case UnitClass.ArchM:
                        case UnitClass.HolyM:
                            Units[i].Skill();
                            break;
                        default:
                            SelectSkill = Units[i];
                            break;
                    }
                }
            }
        }
    }
}
