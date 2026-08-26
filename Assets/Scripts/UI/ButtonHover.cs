using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{

    public bool testValue;
    public Poligraph polygraph;

    public PowerUpType powerUpType;
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        polygraph.Reveal(powerUpType, testValue);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        polygraph.ResetToIdle();
    }
}
