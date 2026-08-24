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
    public bool isToxic;
    public TMP_Text massText;
    private float explosionTimer;
    private Player player;
    
    void Start()
    {
        cargoRb = GetComponent<Rigidbody2D>();
        cargoMass = Random.Range(1 , 15);
        cargoRb.mass = cargoMass;

        PossibilityOfCargoLying();
        PossibilityOfCargoExplosion();
        player = FindFirstObjectByType<Player>();

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
            SceneReload();
        }

        if(player.hasHarmlessCargo && isDangerous)
        {
            isDangerous = false;
            isToxic = true;
        }

        ToxicWaste();

        if(!isDangerous) return;
        if(transform.parent == null) return;
        explosionTimer -= Time.deltaTime;

        if(explosionTimer <= 0f)
        {
            Explode();
            SceneReload();
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
        player.TakeDamage(20);
        Destroy(gameObject);
    }

    public void ToxicWaste()
    {
        //daha sonradan bir timer eklenecek. Aninda canini dusurmeye baslamayacak. Aksine o timer gectikten sonra asagidaki kod cagrilacak.
        if(isToxic && transform.parent != null)
        {
            player.TakeDamage(10f * Time.deltaTime);
        }
    }

    void SceneReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
