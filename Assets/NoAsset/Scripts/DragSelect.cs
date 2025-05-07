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
        if (collision.CompareTag("HitBox"))
        {
            int number = int.Parse(collision.transform.parent.name);
            PlayerManager.instance.SeletedUnits.Add(number);
            collision.transform.parent.GetComponent<SpriteRenderer>().material = Select;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            if (!Close)
            {
                int number = int.Parse(collision.transform.parent.name);
                PlayerManager.instance.SeletedUnits.Remove(number);
                collision.transform.parent.GetComponent<SpriteRenderer>().material = NotSelect;
            }
        }
    }
}
