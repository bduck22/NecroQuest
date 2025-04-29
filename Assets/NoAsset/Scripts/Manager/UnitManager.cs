using UnityEngine;

public class Units : Unit
{

}
public class UnitManager : MonoBehaviour
{
    [SerializeField] Material NotSelect;
    [SerializeField] Material Select;
    [SerializeField] Transform DragOb;
    [SerializeField] Unit Unit;

    Vector2 mouseposition;
    private void Update()
    {
        Vector2 nowmouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonUp(1))
        {
            if (Unit)
            {
                Unit.TargetWid = nowmouse;
                Unit.GetComponent<SpriteRenderer>().material = NotSelect;
                Unit.Move = true;
                Unit = null;
            }
            if (PlayerManager.instance.SeletedUnits.Count > 0)
            {
                for (; PlayerManager.instance.SeletedUnits.Count > 0;)
                {
                    Unit unit = PlayerManager.instance.Units[PlayerManager.instance.SeletedUnits[0]];
                    unit.GetComponent<SpriteRenderer>().material = NotSelect;
                    unit.TargetWid = nowmouse;
                    unit.Move = true;
                    PlayerManager.instance.SeletedUnits.RemoveAt(0);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            foreach(Unit u in PlayerManager.instance.Units)
            {
                u.GetComponent<SpriteRenderer>().material = NotSelect;
            }
            PlayerManager.instance.SeletedUnits.Clear();
            mouseposition = nowmouse;
            RaycastHit2D ray = Physics2D.Raycast(nowmouse, Vector2.zero, 10, LayerMask.GetMask("Unit"));
            if (ray&&ray.transform.CompareTag("Unit"))
            {
                Unit = ray.transform.GetComponent<Unit>();
                PlayerManager.instance.SeletedUnit = Unit;
                Unit.GetComponent<SpriteRenderer>().material = Select;
            }
            else
            {
                if (Unit)
                {
                    Unit.GetComponent<SpriteRenderer>().material = NotSelect;
                    Unit = null;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (mouseposition != nowmouse)
            {
                DragOb.GetComponent<DragSelect>().Close = false;
                DragOb.gameObject.SetActive(true);
                DragOb.position = (mouseposition + nowmouse) / 2;
                DragOb.localScale = new Vector2(Mathf.Abs(nowmouse.x - mouseposition.x), Mathf.Abs(nowmouse.y - mouseposition.y));
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DragOb.GetComponent<DragSelect>().Close = true;
            DragOb.gameObject.SetActive(false);
        }

    }
}
