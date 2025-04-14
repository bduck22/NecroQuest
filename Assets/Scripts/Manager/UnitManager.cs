using UnityEngine;

public class Units : Unit
{

}
public class UnitManager : MonoBehaviour
{
    [SerializeField] Transform DragOb;
    [SerializeField] Unit Unit;

    Vector2 mouseposition;
    private void Awake()
    {
    }
    private void Update()
    {
        Vector2 nowmouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(1))
        {
            if (Unit)
            {
                Unit.TargetWid = nowmouse;
                Unit.Move = true;
                Unit = null;
            }
            if (PlayerManager.instance.SeletedUnits.Count > 0)
            {
                for (; PlayerManager.instance.SeletedUnits.Count>0;)
                {
                    Unit unit = PlayerManager.instance.Units[PlayerManager.instance.SeletedUnits[0]];
                    unit.TargetWid = nowmouse;
                    unit.Move = true;
                    PlayerManager.instance.SeletedUnits.RemoveAt(0);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlayerManager.instance.SeletedUnits.Clear();
            mouseposition = nowmouse;
            RaycastHit2D ray = Physics2D.Raycast(nowmouse, Vector2.zero);
            if (ray)
            {
                Unit = ray.transform.GetComponent<Unit>();
                PlayerManager.instance.SeletedUnit = Unit;
            }
            else Unit = null;
        }
        else if (Input.GetMouseButton(0))
        {
            if (mouseposition != nowmouse)
            {
                DragOb.GetComponent<DragSelect>().Close = false;
                DragOb.gameObject.SetActive(true);
                DragOb.position = (mouseposition + nowmouse) / 2;
                DragOb.localScale = new Vector2(Mathf.Abs(nowmouse.x- mouseposition.x ), Mathf.Abs(nowmouse.y- mouseposition.y ));
            }
        }else if (Input.GetMouseButtonUp(0))
        {
            DragOb.GetComponent<DragSelect>().Close = true;
            DragOb.gameObject.SetActive(false);
        }

    }
}
