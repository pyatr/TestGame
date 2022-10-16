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

    private const float POSITION_TO_SCREEN_RATIO = 0.01f;

    private const int SPAWN_Y_OFFSET = -32;

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

    private List<Balloon> activeBalloons = new List<Balloon>();

    private int spawnRangeX;
    private int spawnRangeY;
    private int lives = 0;

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
    }

    private void OnDisable()
    {
        Balloon.OnBalloonFlewAway -= DecreaseLifeCounter;
        Balloon.OnBalloonDestroyed -= ClearBalloonList;
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
        lives--;
        activeBalloons.Remove(balloon);
        if (lives <= 0)
        {
            gameOverText.SetActive(true);
            StopGame();
        }
    }

    private IEnumerator GameRoutine()
    {
        while (isActiveAndEnabled)
        {
            Balloon spawnedBalloon = balloonPool.Get();
            spawnedBalloon.transform.position = new Vector2(Random.Range(-spawnRangeX + spawnedBalloon.Diameter / POSITION_TO_SCREEN_RATIO, spawnRangeX - spawnedBalloon.Diameter / POSITION_TO_SCREEN_RATIO), -spawnRangeY + SPAWN_Y_OFFSET) * POSITION_TO_SCREEN_RATIO;
            spawnedBalloon.CurrentSpeed *= gameSpeed;
            activeBalloons.Add(spawnedBalloon);
            yield return new WaitForSeconds(spawnedBalloon.RandomSpawnTime);
        }
        StopGame();
    }

    public void StartGame()
    {
        if (gameCoroutine == null)
        {
            lives = startLifeCount;
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
            StopCoroutine(gameCoroutine);
            StopCoroutine(speedGrowth);
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
