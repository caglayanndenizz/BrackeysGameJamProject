using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayMenuMusic();
    }
    public void StartGame()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene("GAME");
    }
}
