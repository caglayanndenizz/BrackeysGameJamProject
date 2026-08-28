using UnityEngine;
using UnityEngine.UI;

public class PowerUpPanel : MonoBehaviour
{
    public GameObject panelObject;
    public Poligraph poligraph;
    public Button healthButton;
    public Button shieldButton; 
    public Button harmlessCargoButton;

    public bool healthIsReal;
    public bool shieldIsReal;
    public bool capacityIsReal;
    public bool harmlessCargoIsReal;

    public float trueChance = 45f; // yuzde 45 dogru.


    public Player player;

    public void ShowPanel()
    {
        poligraph.gameObject.SetActive(true);
        poligraph.ResetToIdle();
        panelObject.SetActive(true);
        Time.timeScale = 0f;

        healthIsReal = Random.Range(0f,100f) < trueChance; // eger uretilen sayi truechance den kucuk olursa true , olmazsa false.
        shieldIsReal = Random.Range(0f,100f) < trueChance;
        capacityIsReal = Random.Range(0f,100f) < trueChance;
        harmlessCargoIsReal = Random.Range(0f,100f) < trueChance;

        healthButton.GetComponent<ButtonHover>().testValue = healthIsReal;
        shieldButton.GetComponent<ButtonHover>().testValue = shieldIsReal;   //buttonHover scriptinin icerisindeki testvalue ya erisiyoruz. Yani o butonun gercek degeri ne
        harmlessCargoButton.GetComponent<ButtonHover>().testValue = harmlessCargoIsReal;


    }

    public void ClosePanel()
    {
        poligraph.gameObject.SetActive(false);
        panelObject.SetActive(false);
        Time.timeScale = 1f;
    }


    public void HealthUpgrade(float health)
    {
        if(healthIsReal)
        {
            player.maxHealth += health;
            player.currentHealth += health;
        }
        else
        {
           player.maxHealth -= health;
           player.currentHealth = Mathf.Clamp(player.currentHealth , 0f , player.maxHealth);


            Cargo cargo = FindFirstObjectByType<Cargo>();
            if(cargo != null)
            {
                cargo.isDangerous = true;
                //cargo.timerText.gameObject.SetActive(true);
                cargo.explosionTimer = 30f;
                cargo.displayOffset =  Random.Range(-3f , 3f);
            }
        }

        player.healthSlider.maxValue = player.maxHealth;
        player.healthSlider.value = player.currentHealth;

        ClosePanel();

    }

    public void ShieldUpgrade()
    {
        player.ActivateShield(shieldIsReal);
        ClosePanel();
    }

    public void HarmlessCargoUpgrade()
    {
        player.harmlessCargoIsSafe = harmlessCargoIsReal;
        player.hasHarmlessCargo = true;
        
        ClosePanel();
    }

}
