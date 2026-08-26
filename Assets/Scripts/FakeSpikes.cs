using UnityEngine;

public class FakeSpikes : MonoBehaviour
{
    public float damage = 10f;
    public float speed = 5f;
    public float lifeTime = 15f;
    public Vector2 direction = Vector2.right;


    void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
