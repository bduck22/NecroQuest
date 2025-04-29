using UnityEngine;

public class DragSelect : MonoBehaviour
{
    public bool Close;
    [SerializeField] Material NotSelect;
    [SerializeField] Material Select;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
            PlayerManager.instance.SeletedUnits.Add(int.Parse(collision.name));
            collision.GetComponent<SpriteRenderer>().material = Select;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
            if (!Close)
            {
                PlayerManager.instance.SeletedUnits.Remove(int.Parse(collision.name));
                collision.GetComponent<SpriteRenderer>().material = NotSelect;
            }
        }
    }
}
