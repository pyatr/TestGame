using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreView : MonoBehaviour
{
    private Text displayText;

    private void Awake()
    {
        displayText = GetComponent<Text>();
    }

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
        displayText.text = newScore.ToString();
    }
}
