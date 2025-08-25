using DamageNumbersPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GoldBase : MonoBehaviour
{
    public int Value;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("HitBox")&&gameObject.activeSelf)|| collision.gameObject.layer == 8||collision.gameObject.layer==6)
        {
            Get();
        }
    }
    public void Get()
    {
        gameObject.SetActive(false);
        GameManager.instance.gold += Value;
        //GoldNumber.Spawn((Vector2)transform.position + new Vector2(0, 1.2f), Value);
    }
}
