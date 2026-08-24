using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public PowerUpPanel powerUpPanel;


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
        if(other.CompareTag("Player"))
        {
            powerUpPanel.ShowPanel();
        }

    }
}
