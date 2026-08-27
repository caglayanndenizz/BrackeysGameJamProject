using System.Collections;
using UnityEngine;

public class RealTrap : MonoBehaviour
{
    public enum SpikeDirection {Up , Down , Right , Left}
    public GameObject spikePrefab;
    public float spawnInterval = 2f;

    public float spikeSpeed;

    public SpikeDirection direction = SpikeDirection.Right;

    public Transform spawnPointUp;
    public Transform spawnPointDown;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;
    void Start()
    {
        StartCoroutine(SpawnSpikes());
    }

    // Update is called once per frame
    IEnumerator SpawnSpikes()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnSpike();
        }
    }

    void SpawnSpike()
    {
        Transform spawnPoint = GetSpawnPoint(direction);
        Vector2 moveDirection = GetVectorFromDirection(direction);

        GameObject spikeObj = Instantiate(spikePrefab, spawnPoint.position , Quaternion.identity);
        RealSpikes spike = spikeObj.GetComponent<RealSpikes>();
        if(spike != null)
        {
            spike.direction = moveDirection;
            spike.speed = spikeSpeed;
        }

        IgnoreCollisionWithOwnTrap(spikeObj);
    }

void IgnoreCollisionWithOwnTrap(GameObject spikeObj)
    {
        Collider2D spikeCollider = spikeObj.GetComponent<Collider2D>();
        Collider2D trapCollider = GetComponent<Collider2D>(); // bu trap'in kendi collider'i

        if(spikeCollider != null && trapCollider != null)
        {
            Physics2D.IgnoreCollision(spikeCollider, trapCollider);
        }
    }

    Transform GetSpawnPoint(SpikeDirection dir)
    {
        switch (dir)
        {
            case SpikeDirection.Up: return spawnPointUp;
            case SpikeDirection.Down: return spawnPointDown;
            case SpikeDirection.Left: return spawnPointLeft;
            case SpikeDirection.Right: return spawnPointRight;
            default: return spawnPointRight;
        }
    }

    Vector2 GetVectorFromDirection(SpikeDirection dir)
    {
        switch (dir)
        {
            case SpikeDirection.Up: return Vector2.up;
            case SpikeDirection.Down: return Vector2.down;
            case SpikeDirection.Right: return Vector2.right;
            case SpikeDirection.Left: return Vector2.left;
            default: return Vector2.right;
        }
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
    }
}
