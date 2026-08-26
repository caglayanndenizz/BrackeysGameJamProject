using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [System.Serializable]
    public struct LevelData
    {
        public GameObject levelPrefab;
        public Vector2 playerSpawnPosition;
    }

    public LevelData[] levels;

    private GameObject currentLevel;
    private int currentLevelIndex = 0;

    public Player player;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int index)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        currentLevelIndex = index;
        currentLevel = Instantiate(levels[currentLevelIndex].levelPrefab);

        ResetPlayer();
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevelIndex);
    }

    public void LoadNextLevel()
    {
        int nextIndex = currentLevelIndex + 1;

        if (nextIndex >= levels.Length)
        {
            SceneManager.LoadScene("Main Menu");
            return;
        }

        LoadLevel(nextIndex);
    }

    void ResetPlayer()
    {
        player.transform.position = levels[currentLevelIndex].playerSpawnPosition;
        player.ResetState();
    }
}