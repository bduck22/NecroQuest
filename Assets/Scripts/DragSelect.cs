using UnityEngine;

public class DragSelect : MonoBehaviour
{
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
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
            PlayerManager.instance.SeletedUnits.Remove(int.Parse(collision.name));
        }
    }
}
