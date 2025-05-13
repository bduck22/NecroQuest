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

    public GameObject SelectSkill;
}
