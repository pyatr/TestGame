using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static event Action OnGameStart = delegate { };
    public static event Action OnGameEnd = delegate { };
    public static event Action<int> OnLifeCounterChanged = delegate { };

    private const float POSITION_TO_SCREEN_RATIO = 0.01f;
    private const int SPAWN_Y_OFFSET = -32;

    private int Lives
    {
        get => lives;
        set
        {
            lives = value;
            OnLifeCounterChanged.Invoke(lives);
        }
    }

    private ObjectPool<Balloon> balloonPool;

    private Coroutine gameCoroutine = null;
    private Coroutine speedGrowth = null;

    [SerializeField]
    private List<Balloon> balloonPrefabTypes;

    [SerializeField]
    private GameObject gameOverText;

    [SerializeField, Range(0.1f, 30f)]
    private float gameSpeed = 1f;
    [SerializeField, Range(0.001f, 50f)]
    private float speedIncrementPerFrame = 0.5f;

    [SerializeField]
    private int startLifeCount = 5;

    [SerializeField]
    private int scoreForExtraLife = 10000;

    private List<Balloon> activeBalloons = new List<Balloon>();

    private int spawnRangeX;
    private int spawnRangeY;
    private int lives = 0;
    private int earnedLives = 0;

    private float startGameSpeed;

    private void Awake()
    {
        balloonPool = new ObjectPool<Balloon>(CreateNewBalloon);
        startGameSpeed = gameSpeed;
        spawnRangeX = Screen.width / 4;
        spawnRangeY = Screen.height / 2;
    }

    private void OnEnable()
    {
        Balloon.OnBalloonFlewAway += DecreaseLifeCounter;
        Balloon.OnBalloonDestroyed += ClearBalloonList;
        ScoreController.OnScoreChanged += GiveLifeFromScore;
    }

    private void OnDisable()
    {
        Balloon.OnBalloonFlewAway -= DecreaseLifeCounter;
        Balloon.OnBalloonDestroyed -= ClearBalloonList;
        ScoreController.OnScoreChanged -= GiveLifeFromScore;
    }

    private void GiveLifeFromScore(int newScore)
    {
        int totalEarned = newScore / scoreForExtraLife;
        if (totalEarned > earnedLives)
        {
            Lives += (totalEarned - earnedLives);
            earnedLives = totalEarned;
        }
    }

    private void ClearBalloonList(Balloon balloon)
    {
        activeBalloons.Remove(balloon);
    }

    private Balloon CreateNewBalloon()
    {
        return Instantiate(balloonPrefabTypes.GetRandomElement());
    }

    private void DecreaseLifeCounter(Balloon balloon)
    {
        Lives--;
        activeBalloons.Remove(balloon);
        if (Lives <= 0)
        {
            gameOverText.SetActive(true);
            StopGame();
        }
    }

    private IEnumerator GameRoutine()
    {
        yield return null;
        Lives = startLifeCount;
        while (isActiveAndEnabled)
        {
            Balloon spawned = SpawnBalloon(balloonPool.Get());
            yield return new WaitForSeconds(spawned.RandomSpawnTime);
        }
        StopGame();
    }

    public Balloon SpawnBalloon(Balloon instance)
    {
        instance.transform.position = new Vector2(Random.Range(-spawnRangeX + instance.Diameter / POSITION_TO_SCREEN_RATIO, spawnRangeX - instance.Diameter / POSITION_TO_SCREEN_RATIO), -spawnRangeY + SPAWN_Y_OFFSET) * POSITION_TO_SCREEN_RATIO;
        instance.CurrentSpeed *= gameSpeed;
        activeBalloons.Add(instance);
        return instance;
    }

    public void StartGame()
    {
        if (gameCoroutine == null)
        {
            gameOverText.SetActive(false);
            gameSpeed = startGameSpeed;
            gameCoroutine = StartCoroutine(GameRoutine());
            speedGrowth = StartCoroutine(SpeedGrowth());
            OnGameStart.Invoke();
        }
    }

    public void StopGame()
    {
        if (gameCoroutine != null)
        {
            activeBalloons.DestroyGameObjects();
            activeBalloons.Clear();
            StopCoroutine(gameCoroutine);
            StopCoroutine(speedGrowth);
            gameCoroutine = null;
            speedGrowth = null;
            OnGameEnd.Invoke();
        }
    }

    private IEnumerator SpeedGrowth()
    {
        while (isActiveAndEnabled)
        {
            gameSpeed += speedIncrementPerFrame * Time.deltaTime;
            yield return null;
        }
    }
}
