using UnityEngine;

public class DragSelect : MonoBehaviour
{
    public bool Close;
    [SerializeField] Material NotSelect;
    [SerializeField] Material Select;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            int number = 0;
            for (int i = 0; i < 4; i++)
            {
                if (PlayerManager.instance.Units[i] == collision.transform.parent.GetComponent<Unit>())
                {
                    number = i; break;
                }
            }
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
                int number = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (PlayerManager.instance.Units[i] == collision.transform.parent.GetComponent<Unit>())
                    {
                        number = i; break;
                    }
                }
                PlayerManager.instance.SeletedUnits.Remove(number);
                collision.transform.parent.GetComponent<SpriteRenderer>().material = NotSelect;
            }
        }
    }
}
