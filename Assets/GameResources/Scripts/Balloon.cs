using UnityEngine;
using UnityEngine.EventSystems;

public class Balloon : AbstractBalloon, IPointerUpHandler, IPointerDownHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        DestroySelf();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //OnPointerUp does not work without OnPointerDown implemented
    }
}
