using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public Animator animator;
    private static readonly int SpeedParam = Animator.StringToHash("Speed");
    private static readonly int IsDeadParam = Animator.StringToHash("IsDead");

    private Rigidbody2D rb;
    private Cargo currentCargo;
    private float baseThrustForce;
    private Coroutine thrustError;
    public float thrustForce;
    public float maxSpeed = 10;    
    public float maxHealth;
    public float currentHealth;
    private bool isDead;

    public Slider healthSlider;
    public Slider shieldSlider;
    public bool isShieldActive = false;
    public bool isShieldPermanent = false;
    public bool canDecay = false;
    public bool hasHarmlessCargo;
    public bool harmlessCargoIsSafe;
    public float maxShield;
    public float currentShield;

    public DeathPanel deathPanel;
    public float deathPanelDelay = 2f;

    public SpriteRenderer spriteRenderer;
    public float hitFlashTimer = 0f;
    public bool isToxicActive;


    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        baseThrustForce = thrustForce;  
    }

    void Update()
    {
        


        if(isDead) return;
        animator.SetFloat(SpeedParam, rb.linearVelocity.magnitude);
        if(Input.GetKeyDown(KeyCode.Space) && currentCargo != null)
        {
            DropCargo();
        }

        if(isShieldActive && !isShieldPermanent && canDecay)
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

        if(hitFlashTimer > 0f)
        {
            hitFlashTimer -= Time.deltaTime;
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = isToxicActive ? Color.green : Color.white;
        }
    }

    void FixedUpdate()
    {  
        
        Move();
    }


    void Move()
    {
        if(isDead) return;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        y = Mathf.Max(y, 0f); //negatif girdi sifirlaniyor.

        Vector2 input = new Vector2(x, y);

        rb.AddForce(input * thrustForce, ForceMode2D.Force);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }
    void DropCargo()
    {   
        //cargo droplandiginda fizik motoru dynamic e degisiyor. Player in child i olmaktan kurtuluyor ve oyuncu mass i eski haline donuyor.
        currentCargo.cargoRb.bodyType = RigidbodyType2D.Dynamic;
        currentCargo.cargoRb.linearVelocity = rb.linearVelocity; //drone un hizini kargonunkine esitliyoruz.
        currentCargo.transform.SetParent(null);
        currentCargo = null;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(currentCargo != null) return; //Eger kargo hala mevcut ise asagidaki satirlari es gec.

        if(other.CompareTag("Cargo"))
        {
            //kargo tag i cargo olan objelerin, player a child olarak ataniyor. Boylece player mass = player mass + cargo mass oluyor.
            currentCargo = other.GetComponent<Cargo>();
            currentCargo.transform.SetParent(transform);
            Debug.Log("Cargo pickup tetiklendi");
            currentCargo.OnPickedUp();
        }
    }

    public void TakeDamage(float damage)
    {

        if(damage >= 1f) hitFlashTimer = 0.1f;
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

            if(currentHealth <= 0 && !isDead)
            {
                isDead = true;
                StartCoroutine(DeathRoutine());

                
            }
        }
        
    }
    private IEnumerator DeathRoutine()
    {
        animator.SetTrigger(IsDeadParam);

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        yield return new WaitForSeconds(deathPanelDelay);

        deathPanel.ShowDeathPanel();
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount , 0f , maxHealth);
        healthSlider.value = currentHealth;
    }

    public void ApplyThrustPenalty(float multiplier , float duration)
    {
        if(thrustError != null)
        {
            StopCoroutine(thrustError);
        }
        thrustError =  StartCoroutine(ThrustError(multiplier , duration));
    }

    private IEnumerator ThrustError(float multiplier , float duration)
    {
        thrustForce = baseThrustForce * multiplier;
        yield return new WaitForSeconds(duration);
        thrustForce = baseThrustForce;
        thrustError = null;
    }

    public void ActivateShield(bool isPermanent)
    {
        isShieldPermanent = isPermanent;
        //health mantigi ile ayni. Shield aktivasyonu saglandiginda once shield in degerini yaziyor daha sonra da slider da guncelliyorsun.
        isShieldActive = true;
        currentShield = maxShield;
        shieldSlider.gameObject.SetActive(true);
        shieldSlider.maxValue = maxShield;
        shieldSlider.value = currentShield;
        Debug.Log("Shield activated: " + isShieldActive);

        if(!isShieldPermanent)
        {
            canDecay = false;
            StartCoroutine(EnableDecayAfterDelay());
        }

    }

    private IEnumerator EnableDecayAfterDelay()
    {
        yield return new WaitForSecondsRealtime(2f);
        canDecay = true;
    }

    public void ResetState()
    {
        StopAllCoroutines();                        
        isDead = false;
        rb.bodyType = RigidbodyType2D.Dynamic;      
        rb.linearVelocity = Vector2.zero;           
        thrustForce = baseThrustForce;              

        if(currentCargo != null)                   
        {
            Destroy(currentCargo.gameObject);
            currentCargo = null;
        }

        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        isShieldActive = false;
        isShieldPermanent = false;
        shieldSlider.gameObject.SetActive(false);
        hasHarmlessCargo = false;                   
        harmlessCargoIsSafe = false;               
        animator.ResetTrigger(IsDeadParam);        
        animator.Play("Idle");
    }
}
