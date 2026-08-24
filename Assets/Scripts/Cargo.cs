using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cargo : MonoBehaviour
{

    public Rigidbody2D cargoRb;
    public int cargoMass;
    public int displayedMass;
    private bool isLying;
    public bool isDangerous;
    public TMP_Text massText;
    private float explosionTimer;
    void Start()
    {
        cargoRb = GetComponent<Rigidbody2D>();
        cargoMass = Random.Range(1 , 15);
        cargoRb.mass = cargoMass;
        
        PossibilityOfCargoLying();
        PossibilityOfCargoExplosion();

        if(isDangerous)
        {
            explosionTimer = Random.Range(20f,50f);
        }
    }

    void Update()
    {

        if(gameObject.transform.position.y <= -20)
        {
            Debug.Log("The ball dropped");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(!isDangerous) return;
        if(transform.parent == null) return;
        explosionTimer -= Time.deltaTime;

        if(explosionTimer <= 0f)
        {
            Explode();
        }
    }

    public void PossibilityOfCargoLying()
    {
        float roll = Random.Range(0f , 1f);
        isLying = roll < 0.4f;

        if(isLying)
        {
            displayedMass = Random.Range(1, 15);
        }
        else
        {
            displayedMass = cargoMass;
        }
        
        massText.text = displayedMass.ToString() + "Kg";
    }

    public void PossibilityOfCargoExplosion()
    {
        isDangerous = Random.Range(0f,1f) < 0.4f;
    }

    void Explode()
    {
        Player carryingPlayer = GetComponentInParent<Player>();
        
        if(carryingPlayer != null)
        {
            carryingPlayer.TakeDamage(20);
        }

        Destroy(gameObject);
    }
}
