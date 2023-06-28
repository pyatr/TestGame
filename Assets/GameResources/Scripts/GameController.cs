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
    private const int SPAWN_Y_OFFSET = -16;

    private int Lives
    {
        get => lives;
        set
        {
            lives = value;
            OnLifeCounterChanged.Invoke(lives);
        }
    }

    private ObjectPool<AbstractBalloon> balloonPool;

    private Coroutine gameCoroutine = null;
    private Coroutine speedGrowth = null;

    [SerializeField]
    private List<AbstractBalloon> balloonPrefabTypes;

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

    private List<AbstractBalloon> activeBalloons = new List<AbstractBalloon>();

    private int spawnRangeX;
    private int spawnRangeY;
    private int lives = 0;
    private int earnedLives = 0;

    private float startGameSpeed;

    private void Awake()
    {
        balloonPool = new ObjectPool<AbstractBalloon>(CreateNewBalloon);
        startGameSpeed = gameSpeed;
        spawnRangeX = Screen.width / 4;
        spawnRangeY = Screen.height / 2;
    }

    private void OnEnable()
    {
        AbstractBalloon.OnBalloonFlewAway += DecreaseLifeCounter;
        AbstractBalloon.OnBalloonDestroyed += ClearBalloonList;
        ScoreController.OnScoreChanged += GiveLifeFromScore;
    }

    private void OnDisable()
    {
        AbstractBalloon.OnBalloonFlewAway -= DecreaseLifeCounter;
        AbstractBalloon.OnBalloonDestroyed -= ClearBalloonList;
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

    private void ClearBalloonList(AbstractBalloon balloon)
    {
        activeBalloons.Remove(balloon);
    }

    private AbstractBalloon CreateNewBalloon()
    {
        return Instantiate(balloonPrefabTypes.GetRandomElement());
    }

    private void DecreaseLifeCounter(AbstractBalloon balloon)
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
        earnedLives = 0;
        Lives = startLifeCount;
        while (isActiveAndEnabled)
        {
            AbstractBalloon spawned = SpawnBalloon(balloonPool.Get());
            yield return new WaitForSeconds(spawned.RandomSpawnTime);
        }
        StopGame();
    }

    public AbstractBalloon SpawnBalloon(AbstractBalloon instance)
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
