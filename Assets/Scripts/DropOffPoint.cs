using UnityEngine;
using UnityEngine.SceneManagement;

public class DropOffPoint : MonoBehaviour
{
    public Player player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Cargo") && other.transform.parent == null)
        {
            Debug.Log("Cargo Received and level completed.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Halihazirda olan scene i tekrardan baslatiyor.
            //ilerde levellari scene olarak yarattigimizda SceneManager.LoadScene("Level bilmem kac") olarak degistirecegiz.

            //eger sure yetmezse SceneManager.LoadScene(SceneManager.GetActiveScene().name) bu
            
        }

        
    }
}
