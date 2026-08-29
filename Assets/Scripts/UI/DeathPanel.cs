using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanel : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;


    public void ShowDeathPanel()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        deathPanel.SetActive(false);
        LevelManager.Instance.RestartLevel();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
