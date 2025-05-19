using UnityEngine;

public class ArrowSkill : MonoBehaviour
{
    bool on=false;
    void Update()
    {
        if (on)
        {
            transform.Translate(20 * Time.deltaTime, 0, 0);
            if (transform.position.x > 50 || transform.position.x < -50 || transform.position.y > 40||transform.position.y<-40) Destroy(gameObject);
        }
    }
    public void ON()
    {
        on = true;
    }
}
