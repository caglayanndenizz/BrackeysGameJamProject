using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour , IPointerEnterHandler
{

    public bool testValue;
    public Poligraph poligraph;

    public PowerUpType powerUpType;
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        poligraph.Reveal(powerUpType, testValue);
    }
}
