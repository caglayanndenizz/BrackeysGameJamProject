using UnityEngine;

public class FakeSpikes : MonoBehaviour
{
    
    public float speed = 5f;
    public float lifetime = 15f;
    public Vector2 direction = Vector2.right;

    public Rigidbody2D fakeRb;
    
    

    void Start()
    {
        Destroy(gameObject , lifetime);
    }



    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
