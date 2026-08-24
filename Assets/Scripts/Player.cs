using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Cargo currentCargo;
    public float thrustForce;
    public float maxSpeed = 10;
    public float emptyMass;
    public float totalMass;
    public float droneCapacity;
    public float capacity;
    
    public float maxHealth;
    public float currentHealth;

    public Slider healthSlider;
    public Slider shieldSlider;
    public bool isShieldActive = false;
    public bool hasHarmlessCargo;
    public float maxShield;
    public float currentShield;


    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = emptyMass;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        

        
        
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && currentCargo != null)
        {
            DropCargo();
        }

        if(isShieldActive)
        {
            //shield decay mantigi.
            currentShield -= 15f * Time.deltaTime;
            currentShield = Mathf.Clamp(currentShield,0f,maxShield);
            shieldSlider.value = currentShield;
            if(currentShield <= 0)
            {
                isShieldActive = false;
            }
        }
    }

    void FixedUpdate()
    {  
        Move();
    }


    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(x, y);

        if(currentCargo != null)
        {
            totalMass = emptyMass + currentCargo.cargoMass;
        }
        else
        {
            totalMass = emptyMass;
        }
        
        droneCapacity = totalMass/capacity;

        float appliedForce = thrustForce;
        
        if(droneCapacity >= 2f)

        {
            appliedForce = thrustForce * 0.25f; //%75 kesinti yapiyor. 
        }
        else if(droneCapacity > 1f)
        {
            appliedForce = thrustForce * 0.5f; //%50 kesinti yapiyor.
        }

        rb.AddForce(input * appliedForce, ForceMode2D.Force);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    void DropCargo()
    {   
        //cargo droplandiginda fizik motoru dynamic e degisiyor. Player in child i olmaktan kurtuluyor ve oyuncu mass i eski haline donuyor.
        currentCargo.cargoRb.bodyType = RigidbodyType2D.Dynamic;
        currentCargo.cargoRb.linearVelocity = rb.linearVelocity; //drone un hizini kargonunkine esitliyoruz.
        currentCargo.transform.SetParent(null);
        Debug.Log("DropCargo calisti, SetParent sonrasi parent: " + currentCargo.transform.parent);
        currentCargo = null;
        rb.mass = emptyMass;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(currentCargo != null) return; //Eger kargo hala mevcut ise asagidaki satirlari es gec.

        if(other.CompareTag("Cargo"))
        {
            //kargo tag i cargo olan objelerin, player a child olarak ataniyor. Boylece player mass = player mass + cargo mass oluyor.
            currentCargo = other.GetComponent<Cargo>();
            rb.mass = totalMass;
            currentCargo.transform.SetParent(transform);
        }
    }

    public void TakeDamage(float damage)
    {
        if(isShieldActive)
        {
            currentShield = Mathf.Clamp(currentShield - damage , 0f , maxShield);
            shieldSlider.value = currentShield;
            if(currentShield <=0)
            {
                isShieldActive = false;
                shieldSlider.gameObject.SetActive(false);
                Debug.Log("Shield depleted.");
            }
        }
        else
        {
            currentHealth = Mathf.Clamp(currentHealth - damage , 0f , maxHealth);
            //Mathf.Clamp = min max degerini kesinlestiriyor. Canin -501239 ye inmesini engelliyor mesela.
            healthSlider.value = currentHealth;

            if(currentHealth <= 0)
            {
                Debug.Log("Dead");
            }
        }
        
    }

    public void ActivateShield()
    {
        //health mantigi ile ayni. Shield aktivasyonu saglandiginda once shield in degerini yaziyor daha sonra da slider da guncelliyorsun.
        isShieldActive = true;
        currentShield = maxShield;
        shieldSlider.gameObject.SetActive(true);
        shieldSlider.maxValue = maxShield;
        shieldSlider.value = currentShield;
        Debug.Log("Shield activated: " + isShieldActive);

    }
}
