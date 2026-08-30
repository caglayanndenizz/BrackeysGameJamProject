using UnityEngine;
using UnityEngine.SceneManagement;

public class DropOffPoint : MonoBehaviour
{
    private bool levelCompleted = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        CheckCargo(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        CheckCargo(other);
    }

    void CheckCargo(Collider2D other)
    {
        if (levelCompleted) return;

        if (other.CompareTag("Cargo") && other.transform.parent == null)
        {
            levelCompleted = true;
            AudioManager.instance.Play(AudioManager.instance.levelCompleted);
            Destroy(other.gameObject);
            LevelManager.Instance.LoadNextLevel();
        }
    }
}