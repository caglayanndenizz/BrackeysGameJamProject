using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject cargoPrefab;
    public Transform pickUpTransform;


    void Start()
    {
        SpawnCargo();
    }

    void SpawnCargo()
    {
        if (pickUpTransform == null)
        {
            return;
        }

        
        Vector2 spawnPosition = new Vector2(pickUpTransform.position.x, pickUpTransform.position.y);
        Instantiate(cargoPrefab , spawnPosition , Quaternion.identity, transform);
    }

}