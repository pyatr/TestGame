using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static event Action<int> OnScoreChanged = delegate { };

    public int Score { private get; set; } = 0;

    private void OnEnable()
    {
        GameController.OnGameEnd += SaveScore;
        AbstractBalloon.OnBalloonDestroyed += AddScore;
    }

    private void OnDisable()
    {
        GameController.OnGameEnd -= SaveScore;
        AbstractBalloon.OnBalloonDestroyed -= AddScore;
    }

    private void AddScore(AbstractBalloon balloon)
    {
        AddScore(balloon.Score);
    }

    private void AddScore(int score)
    {
        Score += score;
        OnScoreChanged.Invoke(Score);
    }

    private void SaveScore()
    {
        RecordController.SaveRecord(PlayerProfile.PlayerName, Score);
        AddScore(-Score);
    }
}
