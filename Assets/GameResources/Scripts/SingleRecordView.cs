using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleRecordView : MonoBehaviour
{
    public string PlayerNameText
    {
        get => playerNameText.text;
        set => playerNameText.text = value;
    }

    public string PlayerScoreText
    {
        get => playerScoreText.text;
        set => playerScoreText.text = value;
    }

    [SerializeField]
    private Text playerNameText;

    [SerializeField]
    private Text playerScoreText;
}
