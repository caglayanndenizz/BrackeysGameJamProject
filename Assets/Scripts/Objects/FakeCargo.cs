using UnityEngine;

public class FakeCargo : MonoBehaviour
{
    public float healAmount;
    public float healChance ;

    public float thrustMultiplier;
    public float thrustDuration;

    private Player player;


    void Awake()
    {
        player = FindAnyObjectByType<Player>();

        if(player == null)
        {
            Debug.LogWarning("FakeCargo: sahnede Player bulunamadi.");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(player == null) return;

            if(Random.value <= healChance)
            {
                player.Heal(healAmount);
            }
            else
            {
                player.ApplyThrustPenalty(thrustMultiplier, thrustDuration);
            }

            Destroy(gameObject);
        }
    }
}
