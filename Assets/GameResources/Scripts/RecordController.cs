using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordController : MonoBehaviour
{
    private const string PLAYER_RECORDS_PREFS_KEY = "PlayerRecord";

    private const char RECORDS_DELIMITER = '-';
    private const char PLAYERS_DATA_DELIMITER = ':';

    private string PlayerRecords => PlayerPrefs.GetString(PLAYER_RECORDS_PREFS_KEY, string.Empty);

    public void SaveRecord(string playerName, int score)
    {
        PlayerPrefs.SetString(PLAYER_RECORDS_PREFS_KEY, $"{PlayerRecords}-");
    }

    public List<KeyValuePair<string, string>> GetPlayersRecords()
    {
        List<KeyValuePair<string, string>> preparedRecords = new List<KeyValuePair<string, string>>();
        string records = PlayerRecords;
        if (records != string.Empty)
        {
            string[] splitRecords = records.Split(RECORDS_DELIMITER);
            foreach (string record in splitRecords)
            {
                string[] recordData = record.Split(PLAYERS_DATA_DELIMITER);
                preparedRecords.Add(new KeyValuePair<string, string>(recordData[0], recordData[1]));
            }
        }
        return preparedRecords;
    }
}
