using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CircleCollider2D))]
public class Balloon : MonoBehaviour, IPointerUpHandler
{
    public static event Action<Balloon> OnBalloonDestroyed = delegate { };

    /// <summary>
    /// —лучайное ожидание перед по€влением этого шарика
    /// </summary>
    public float RandomSpawnTime => Random.Range(minSpawnTime, maxSpawnTime);

    public float Radius => balloonCollider.radius;

    public float Diameter => balloonCollider.radius * 2;

    public float RandomSpeed => Random.Range(minMoveSpeed, maxMoveSpeed);

    [SerializeField]
    protected float minSpawnTime = 1.2f;

    [SerializeField]
    protected float maxSpawnTime = 3f;

    [SerializeField]
    protected float minMoveSpeed = 0.08f;

    [SerializeField]
    protected float maxMoveSpeed = 0.15f;

    [SerializeField]
    protected int score;

    protected CircleCollider2D balloonCollider;

    protected float currentSpeed;

    protected virtual void Awake()
    {
        balloonCollider = GetComponent<CircleCollider2D>();
    }

    protected virtual void OnEnable()
    {
        currentSpeed = RandomSpeed;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        FireDestroyedEvent();
    }

    protected void FireDestroyedEvent()
    {
        OnBalloonDestroyed.Invoke(this);
    }

    private void Update()
    {
        transform.MoveBy(0, currentSpeed);
    }
}
