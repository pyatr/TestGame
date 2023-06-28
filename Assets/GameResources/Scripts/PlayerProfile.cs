using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerProfile
{
    private const string PLAYER_NAME_PREFS_KEY = "CurrentPlayerName";

    public static string PlayerName
    {
        get => PlayerPrefs.GetString(PLAYER_NAME_PREFS_KEY, "Player");
        set => PlayerPrefs.SetString(PLAYER_NAME_PREFS_KEY, value);
    }
}
