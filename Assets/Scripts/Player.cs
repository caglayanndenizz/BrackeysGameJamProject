using UnityEngine;
public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Cargo currentCargo;
    public float thrustForce;
    public float emptyMass;
    public float droneCapacity = 10f;
    public float capacity;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = emptyMass;
        
        
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && currentCargo != null)
        {
            DropCargo();
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

        droneCapacity = rb.mass/capacity;

        float appliedForce = thrustForce;
        
        if(droneCapacity >= 15f)

        {
            appliedForce = thrustForce * 0.5f;
        }

        rb.AddForce(input * appliedForce, ForceMode2D.Impulse);
    }

    void DropCargo()
    {   
        //cargo droplandiginda fizik motoru dynamic e degisiyor. Player in child i olmaktan kurtuluyor ve oyuncu mass i eski haline donuyor.
        currentCargo.cargoRb.bodyType = RigidbodyType2D.Dynamic;
        currentCargo.cargoRb.linearVelocity = rb.linearVelocity; //drone un hizini kargonunkine esitliyoruz.
        currentCargo.transform.SetParent(null);
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
            rb.mass = currentCargo.cargoMass + rb.mass;
            currentCargo.transform.SetParent(transform);
        }
    }
}
