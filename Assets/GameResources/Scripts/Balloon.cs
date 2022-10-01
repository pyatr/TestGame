using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Balloon : MonoBehaviour, IPointerUpHandler
{
    public static event Action<Balloon> OnBalloonDestroyed = delegate { };

    [SerializeField]
    protected float minSpawnTime = 1.2f;

    [SerializeField]
    protected float maxSpawnTime = 3f;

    [SerializeField]
    protected int score;

    protected Collider2D balloonCollider;


    protected virtual void Awake()
    {
        balloonCollider = GetComponent<Collider2D>();

    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        FireDestroyedEvent();
    }

    protected void FireDestroyedEvent()
    {
        OnBalloonDestroyed.Invoke(this);
    }
}
