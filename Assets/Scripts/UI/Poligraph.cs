using UnityEngine;

public class Poligraph : MonoBehaviour
{
     [SerializeField] private bool usedThisLevel = false;
     public Animator animator;
     public GameObject errorImage;

    void Start()
    {
        errorImage.SetActive(false);
    }
    public void Reveal(PowerUpType type , bool realValue)
    {
        if(usedThisLevel) return;

        float accuracy = GameManager.instance.GetAccuracy();

        float random = Random.Range(0f , 100f);
        
        bool displayedValue;

        if(random < accuracy)
        {
            displayedValue = realValue;
        } 
        else
        {
            displayedValue = !realValue;
        }

        animator.SetBool("IsWavy" , !displayedValue);
        usedThisLevel = true;
        GameManager.instance.IncreaseCount();
    }


    public void ResetToIdle()
    {
        animator.SetBool("IsWavy" , false);
        errorImage.SetActive(usedThisLevel);
    }

     public void ResetForNewLevel()
    {
        usedThisLevel = false;
        errorImage.SetActive(false);
    }

}

   
