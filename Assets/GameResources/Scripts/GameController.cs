using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameController : MonoBehaviour
{
    private ObjectPool<Balloon> balloonPool;

    private Coroutine gameCoroutine = null;

    private void Awake()
    {
        balloonPool = new ObjectPool<Balloon>(CreateNewBalloon);
    }

    private Balloon CreateNewBalloon()
    {
        return null;
    }

    private IEnumerator Game()
    {
        yield return null;
    }

    private void StartGame()
    {
        if (gameCoroutine == null)
        {
            gameCoroutine = StartCoroutine(Game());
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
