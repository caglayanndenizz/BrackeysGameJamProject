using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cargo : MonoBehaviour
{

    public Rigidbody2D cargoRb;
    public int cargoMass;
    public int displayedMass;
    private bool isLying;
    public TMP_Text massText;
    void Start()
    {
        cargoRb = GetComponent<Rigidbody2D>();
        cargoMass = Random.Range(1 , 15);
        cargoRb.mass = cargoMass;
        
        float roll = Random.Range(0f , 1f);
        isLying = roll < 0.6f;

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

    void Update()
    {
        if(gameObject.transform.position.y <= -20)
        {
            Debug.Log("The ball dropped");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
