using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public int usageCount = 0;
    public float baseAccuracy = 100f;
    public float decayPerUse = 10f;
    public float minAccuracy = 50f;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    
    public float GetAccuracy() // icerisinde hesaplama yapiyor ve hesaplama sonrasi degeri geri gonderiyor. O yuzden void kullanilmadi cunku void 1 kere cagriliyor.
    {
        float accuracy = baseAccuracy - (usageCount * decayPerUse);
        return Mathf.Max(accuracy , minAccuracy);
    }

    public void IncreaseCount()
    {
        usageCount++;
    }
}
