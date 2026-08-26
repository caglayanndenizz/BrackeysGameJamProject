using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public PowerUpPanel powerUpPanel;
    private bool panelShown = false;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            powerUpPanel.ClosePanel();
            Time.timeScale = 1f;
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
