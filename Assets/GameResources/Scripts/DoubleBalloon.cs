using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleBalloon : AbstractBalloon, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private AbstractBalloon balloonSpawnedOnClick;

    protected GameController game;

    protected override void OnEnable()
    {
        base.OnEnable();
        game = FindObjectOfType<GameController>();
    }

    private void SpawnBalloon(string spawnAnimation)
    {
        AbstractBalloon b = Instantiate(balloonSpawnedOnClick);
        b.Animator.SetBool(spawnAnimation, true);
        game.SpawnBalloon(b);
        b.transform.position = transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SpawnBalloon("AppearLeft");
        SpawnBalloon("AppearRight");
        DestroySelf();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //OnPointerUp does not work without OnPointerDown implemented
    }
}
