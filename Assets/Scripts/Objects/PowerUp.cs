using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public PowerUpPanel powerUpPanel;
    private bool panelShown = false;


    void Awake()
    {
        if(powerUpPanel == null)
        {
            powerUpPanel = FindFirstObjectByType<PowerUpPanel>(FindObjectsInactive.Include);

            if(powerUpPanel == null)
            {
                Debug.LogWarning("PowerUp: Sahnede PowerUpPanel bulunamadi.");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(panelShown) return;
        if(other.CompareTag("Player"))
        {
            panelShown = true;
            powerUpPanel.ShowPanel();
        }

    }
}
