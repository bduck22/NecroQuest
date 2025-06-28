using Unity.VisualScripting;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public Material NotSelect;
    public Material Select;
    [SerializeField] Transform DragOb;
    public Transform SkillRange;
    [SerializeField] Transform RightCursor;

    Vector2 mouseposition;
    PlayerManager PlayerManager;

    public Texture2D Origin;
    public Texture2D Move;
    public Texture2D Skill;

    public Transform Setting;

    private void Start()
    {
        //guardians = new List<Guardian>();
        PlayerManager = GetComponent<PlayerManager>();
        Cursor.SetCursor(Origin, Vector2.zero, CursorMode.Auto);
    }
    void SelectClear()
    {
        foreach (Unit u in PlayerManager.Units)
        {
            if (u != null)
            {
                u.GetComponent<SpriteRenderer>().material = NotSelect;
            }
        }
        PlayerManager.SeletedUnits.Clear();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Setting.gameObject.SetActive(!Setting.gameObject.activeSelf);
            if (Setting.gameObject.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        Vector2 nowmouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            RightCursor.position = nowmouse;
            RightCursor.GetComponent<Animator>().Play("Click");
        }
        if (Input.GetMouseButton(1))
        {
            if (PlayerManager.SeletedUnits.Count > 0)
            {
                Cursor.SetCursor(Move, Vector2.zero, CursorMode.Auto);
            }

            if (PlayerManager.SelectSkill)
            {
                PlayerManager.SelectSkill = null;
                SkillRange.gameObject.SetActive(false);
            }

            if (PlayerManager.SeletedUnits.Count > 0)
            {
                foreach (int num in PlayerManager.SeletedUnits)
                {
                    Unit unit = PlayerManager.Units[num];
                    if (unit.TargetWid != nowmouse)
                    {
                        unit.TargetWid = nowmouse;
                    }
                    unit.Move = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.SetCursor(Origin, Vector2.zero, CursorMode.Auto);
            RightCursor.position = nowmouse;
            RightCursor.GetComponent<Animator>().Play("Click");
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (!PlayerManager.SelectSkill)
            {
                mouseposition = nowmouse;
                RaycastHit2D ray = Physics2D.Raycast(nowmouse, Vector2.zero, 10, LayerMask.GetMask("Unit"));
                if (ray && ray.transform.CompareTag("Unit"))
                {
                    int number = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (PlayerManager.instance.Units[i] == ray.transform.GetComponent<Unit>())
                        {
                            number = i; break;
                        }
                    }
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        ray.transform.GetComponent<SpriteRenderer>().material = Select;
                        PlayerManager.instance.SeletedUnits.Add(number);
                    }
                    else if (Input.GetKey(KeyCode.LeftControl))
                    {
                        ray.transform.GetComponent<SpriteRenderer>().material = NotSelect;
                        PlayerManager.instance.SeletedUnits.Remove(number);
                    }
                    else
                    {
                        SelectClear();
                        ray.transform.GetComponent<SpriteRenderer>().material = Select;
                        PlayerManager.instance.SeletedUnits.Add(number);
                    }
                }
                else
                {
                    SelectClear();
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (!PlayerManager.SelectSkill && mouseposition != nowmouse)
            {
                DragOb.GetComponent<DragSelect>().Close = false;
                DragOb.gameObject.SetActive(true);
                DragOb.position = (mouseposition + nowmouse) / 2;
                DragOb.localScale = new Vector2(Mathf.Abs(nowmouse.x - mouseposition.x), Mathf.Abs(nowmouse.y - mouseposition.y));
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(Origin, Vector2.zero, CursorMode.Auto);
            DragOb.GetComponent<DragSelect>().Close = true;
            DragOb.gameObject.SetActive(false);
            if (PlayerManager.SelectSkill)
            {
                if (PlayerManager.SelectSkill.gameObject.activeSelf)
                {
                    Debug.Log("마우스 지정 스킬");
                    PlayerManager.SelectSkill.Skill();
                    PlayerManager.SelectSkill = null;
                    SkillRange.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("해당 유닛 사망함");
                    PlayerManager.SelectSkill = null;
                    SkillRange.gameObject.SetActive(false);
                }
            }
        }
        if (PlayerManager.SelectSkill)
        {
            Cursor.SetCursor(Skill, Vector2.zero, CursorMode.Auto);
            SelectClear();
            if (!SkillRange.gameObject.activeSelf)
            {
                SkillRange.gameObject.SetActive(true);
            }
            SkillRange.position = nowmouse;
            switch (PlayerManager.SelectSkill.UnitClass)
            {
                case UnitClass.ArchM:
                    float stack = PlayerManager.SelectSkill.Buff[0].Value * 0.02f + 1;
                    SkillRange.localScale = new Vector3(stack * 2, 2 * stack, 1);
                    SkillRange.localRotation = Quaternion.Euler(45, 0, 0);
                    break;
                case UnitClass.HolyM:
                    SkillRange.localScale = new Vector3(5.7f, 5.7f, 1);
                    SkillRange.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
            }
        }
    }
}