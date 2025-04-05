using UnityEngine;

public class Units : Unit
{

}
public class UnitManager : MonoBehaviour
{
    [SerializeField] Transform DragOb;
    Unit Unit;

    Vector2 mouseposition;
    private void Awake()
    {
        Unit = PlayerManager.instance.SeletedUnit;
    }
    private void Update()
    {
        Vector2 nowmouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Unit && Input.GetMouseButtonDown(1))
        {
            Unit.TargetWid = nowmouse;
            Unit.Move = true;
            Unit = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseposition = nowmouse;
            RaycastHit2D ray = Physics2D.Raycast(nowmouse, Vector2.zero);
            if (ray)
            {
                Unit = ray.transform.GetComponent<Unit>();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (mouseposition != nowmouse)
            {
                DragOb.gameObject.SetActive(true);
                DragOb.position = (mouseposition + nowmouse) / 2;
                DragOb.localScale = new Vector2(Mathf.Abs(nowmouse.x- mouseposition.x ), Mathf.Abs(nowmouse.y- mouseposition.y ));
            }
        }else if (Input.GetMouseButtonUp(0))
        {
            DragOb.gameObject.SetActive(false);
        }
    }
}
