using UnityEngine;

public class Cargo : MonoBehaviour
{

    public Rigidbody2D cargoRb;
    public float cargoMass;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cargoRb = GetComponent<Rigidbody2D>();
        cargoMass = Random.Range(1,10);
        cargoRb.mass = cargoMass;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
