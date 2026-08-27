using UnityEngine;

public class RealSpikes : MonoBehaviour
{

    public float damage = 10f;
    public float speed = 5f;
    public float lifeTime = 15f;
    public Vector2 direction = Vector2.right;
    
    public Rigidbody2D rb;
    

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.TakeDamage(10);
            }
            
        }
        Destroy(gameObject);
    }


}
