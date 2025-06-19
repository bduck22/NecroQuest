using UnityEngine;

public class gold : MonoBehaviour
{
    Vector2 goldposition;
    bool updown;
    private void OnEnable()
    {
        goldposition = transform.position;
    }
    void Update()
    {
        if (!updown)
        {
            if (goldposition != (Vector2)transform.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, goldposition, Time.deltaTime*1);
            }
            else
            {
                updown = true;
            }
        }
        else
        {
            if (goldposition + new Vector2(0, 0.25f) != (Vector2)transform.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, goldposition + new Vector2(0, 0.25f), Time.deltaTime*1);
            }
            else
            {
                updown = false;
            }
        }
    }
}