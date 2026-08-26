using UnityEngine;

public class Poligraph : MonoBehaviour
{
     [SerializeField] private bool usedBefore = false;
     public Animator animator;
    public void Reveal(PowerUpType type , bool realValue)
    {
        if(usedBefore) return;

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
        usedBefore = true;
        GameManager.instance.IncreaseCount();
    }


    public void ResetToIdle()
    {
        animator.SetBool("IsWavy" , false);
    }

}

   
