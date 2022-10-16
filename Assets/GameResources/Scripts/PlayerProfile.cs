using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfile : MonoBehaviour
{
    private const string PLAYER_NAME_PREFS_KEY = "CurrentPlayerName";

    public string PlayerName
    {
        get => PlayerPrefs.GetString(PLAYER_NAME_PREFS_KEY, "Player");
        set => PlayerPrefs.SetString(PLAYER_NAME_PREFS_KEY, value);
    }

    [SerializeField]
    private InputField nameInput;

    private void Awake()
    {
        nameInput.text = PlayerName;
    }
}
