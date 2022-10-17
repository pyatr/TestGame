using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : AbstractTextView
{
    private void OnEnable()
    {
        ScoreController.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        ScoreController.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int newScore)
    {
        viewText.text = newScore.ToString();
    }
}
