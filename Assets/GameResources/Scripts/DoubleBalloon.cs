using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleBalloon : Balloon
{
    [SerializeField]
    private Balloon balloonSpawnedOnClick;

    protected GameController game;

    protected override void OnEnable()
    {
        base.OnEnable();
        game = FindObjectOfType<GameController>();
    }

    private void SpawnBalloon(string spawnAnimation)
    {
        Balloon b = Instantiate(balloonSpawnedOnClick);
        b.Animator.SetBool(spawnAnimation, true);
        game.SpawnBalloon(b);
        b.transform.position = transform.position;
    }

    protected override void OnUp(PointerEventData eventData)
    {
        SpawnBalloon("AppearLeft");
        SpawnBalloon("AppearRight");
        base.OnUp(eventData);
    }
}
