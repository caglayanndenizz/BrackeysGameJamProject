using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cargo : MonoBehaviour
{

    public Rigidbody2D cargoRb;
    public bool isDangerous;
    public bool isToxic;
    
    public float explosionTimer;
    private Player player;
    public TMP_Text timerText;

    public float displayOffset;
    
    void Start()
    {
        cargoRb = GetComponent<Rigidbody2D>();

        PossibilityOfCargoExplosion();
        player = FindFirstObjectByType<Player>();


        timerText = GameObject.FindGameObjectWithTag("TimerUI").GetComponent<TMP_Text>();
        timerText.text = "";
        
        
        if(isDangerous)
        {
            
            explosionTimer = 30f;
            displayOffset = Random.Range(-3f , 3f);
        }


        
    }

    void Update()
    {

        /*if(gameObject.transform.position.y <= -20)
        {
            Debug.Log("The ball dropped");
            SceneReload();
        }
        */

        if(player.hasHarmlessCargo)
        {
            isToxic = !player.harmlessCargoIsSafe;

            if(isDangerous)
            {
                isDangerous = false;
                timerText.gameObject.SetActive(false);
            }
        }

        ToxicWaste();

        if(!isDangerous) return;
        if(transform.parent == null) return;
        explosionTimer -= Time.deltaTime;

        float displayedTimer = Mathf.Max(explosionTimer + displayOffset , 0f); // timer in - li sayi almasini engelliyor.
        timerText.text = explosionTimer.ToString("F0"); //23.45 gibi bir sayiyi 23 e tamamlar.

        if(explosionTimer <= 0f)
        {
            Explode();
            SceneReload();
        } 
    }


    public void PossibilityOfCargoExplosion()
    {
        isDangerous = Random.Range(0f,1f) < 0.4f;
    }

    void Explode()
    {
        player.TakeDamage(300);
        Destroy(gameObject);
    }

    public void ToxicWaste()
    {
        //daha sonradan bir timer eklenecek. Aninda canini dusurmeye baslamayacak. Aksine o timer gectikten sonra asagidaki kod cagrilacak.
        if(isToxic && transform.parent != null)
        {
            player.TakeDamage(2.5f * Time.deltaTime);
        }
    }
    void SceneReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnPickedUp()
    {
        timerText.gameObject.SetActive(isDangerous);
    }
}
