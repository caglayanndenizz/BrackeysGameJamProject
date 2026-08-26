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

        float offSetX = Random.Range(0.5f , 2f);
        Vector2 spawnPosition = new Vector2(pickUpTransform.position.x + offSetX , pickUpTransform.position.y + offSetX);
        Instantiate(cargoPrefab , spawnPosition , Quaternion.identity);
    }

}