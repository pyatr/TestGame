using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesView : AbstractTextView
{
    private void OnEnable()
    {
        GameController.OnLifeCounterChanged += UpdateLifeCounter;
    }

    private void OnDisable()
    {
        GameController.OnLifeCounterChanged -= UpdateLifeCounter;
    }

    private void UpdateLifeCounter(int newLifeCount)
    {
        viewText.text = newLifeCount.ToString();
    }
}
