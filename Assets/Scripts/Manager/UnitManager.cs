using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
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
    }
    private void Update()
    {
        if (PlayerManager.instance.SeletedUnit&&Input.GetMouseButtonDown(1))
        {
            PlayerManager.instance.SeletedUnit.GetComponent<Unit>().TargetWid = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlayerManager.instance.SeletedUnit.GetComponent<Unit>().Move = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (ray)
            {
                PlayerManager.instance.SeletedUnit = ray.transform.gameObject;
            }
        }
    }
}
