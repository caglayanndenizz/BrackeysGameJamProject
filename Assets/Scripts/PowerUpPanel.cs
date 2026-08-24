using UnityEngine;
using UnityEngine.UI;

public class PowerUpPanel : MonoBehaviour
{
    public GameObject panelObject;
    public Button healthButton;
    public Button shildButton;
    public Button capacityButton;
    public Button harmlessCargoButton;

    public Player player;

    void Start()
    {

    }
    public void ShowPanel()
    {
        panelObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClosePanel()
    {
        panelObject.SetActive(false);
        Time.timeScale = 0f;
    }


    public void HealthUpgrade(float health)
    {
        player.maxHealth += health;
        player.currentHealth += health;
        player.healthSlider.maxValue = player.maxHealth;
        player.healthSlider.value = player.currentHealth;

        gameObject.SetActive(false);
        Time.timeScale = 1f;

        //buraya da ekstra bir twist ekle. Health kazanirsin fakat bombadan korumaz mesela.
    }

    public void ShieldUpgrade()
    {
        player.ActivateShield();
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void HarmlessCargoUpgrade()
    {
        // Kargo nun patlamamasini garanti ediyor.?
    }

    public void CapacityUpgrade()
    {
        //Capacity yi  upgrade edersin fakat only if displayedMass = cargoMass
    }
}
