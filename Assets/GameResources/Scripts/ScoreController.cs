using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static event Action<int> OnScoreChanged = delegate { };

    public int Score { private get; set; } = 0;

    [SerializeField]
    private RecordController recordController;

    [SerializeField]
    private PlayerProfile profile;

    private void OnEnable()
    {
        GameController.OnGameEnd += SaveScore;
        Balloon.OnBalloonDestroyed += AddScore;
    }

    private void OnDisable()
    {
        GameController.OnGameEnd -= SaveScore;
        Balloon.OnBalloonDestroyed -= AddScore;
    }

    private void AddScore(Balloon balloon)
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
        recordController.SaveRecord(profile.PlayerName, Score);
        AddScore(-Score);
    }
}
