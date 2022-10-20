using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Balloon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static event Action<Balloon> OnBalloonDestroyed = delegate { };
    public static event Action<Balloon> OnBalloonFlewAway = delegate { };

    private static byte orderInLayer = 0;

    public Animator Animator => animator;

    public float RandomSpawnTime => Random.Range(minSpawnTime, maxSpawnTime);

    public float Diameter => diameter;

    public float RandomSpeed => Random.Range(minMoveSpeed, maxMoveSpeed);

    public int Score => score;

    public float CurrentSpeed;

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    protected float minSpawnTime = 1.2f;

    [SerializeField]
    protected float maxSpawnTime = 3f;

    [SerializeField]
    protected float minMoveSpeed = 0.08f;

    [SerializeField]
    protected float maxMoveSpeed = 0.15f;

    [SerializeField]
    protected float diameter = 1f;

    [SerializeField]
    protected int score;

    protected SpriteRenderer spriteRenderer;

    private bool hasAppeared = false;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        CurrentSpeed = RandomSpeed;
        spriteRenderer.sortingOrder = orderInLayer++;
        hasAppeared = false;
    }

    protected virtual void DestroySelf()
    {
        OnBalloonDestroyed.Invoke(this);
        Destroy(gameObject);
    }

    protected virtual void Update()
    {
        if (spriteRenderer.isVisible)
        {
            hasAppeared = true;
        }
        else if (hasAppeared)
        {
            OnBalloonFlewAway.Invoke(this);
            Destroy(gameObject);
        }
    }

    protected virtual void FixedUpdate()
    {
        transform.MoveBy(0, CurrentSpeed);
    }

    protected virtual void OnUp(PointerEventData eventData)
    {
        DestroySelf();
    }

    protected virtual void OnDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnUp(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDown(eventData);
    }
}
