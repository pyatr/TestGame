using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameController : MonoBehaviour
{
    private const float POSITION_TO_SCREEN_RATIO = 0.01f;

    private const int SPAWN_Y_OFFSET = -32;

    private ObjectPool<Balloon> balloonPool;

    private Coroutine gameCoroutine = null;

    [SerializeField] private List<Balloon> balloonPrefabTypes;

    private int spawnRangeX;
    private int spawnRangeY;

    private void Awake()
    {
        balloonPool = new ObjectPool<Balloon>(CreateNewBalloon);
        spawnRangeX = Screen.width / 4;
        spawnRangeY = Screen.height / 2;
    }

    private void Start()
    {
        StartGame();
    }

    private Balloon CreateNewBalloon()
    {
        return Instantiate(balloonPrefabTypes.GetRandomElement());
    }

    private IEnumerator GameRoutine()
    {
        while (isActiveAndEnabled)
        {
            Balloon spawnedBalloon = balloonPool.Get();
            spawnedBalloon.transform.position = new Vector2(Random.Range(-spawnRangeX + spawnedBalloon.Diameter, spawnRangeX - spawnedBalloon.Diameter), -spawnRangeY + SPAWN_Y_OFFSET) * POSITION_TO_SCREEN_RATIO;
            yield return new WaitForSeconds(spawnedBalloon.RandomSpawnTime);
        }
        StopGame();
    }

    private void StartGame()
    {
        if (gameCoroutine == null)
        {
            gameCoroutine = StartCoroutine(GameRoutine());
        }
    }

    private void StopGame()
    {
        if (gameCoroutine != null)
        {
            StopCoroutine(gameCoroutine);
        }
    }

}
