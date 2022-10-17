using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecordController : MonoBehaviour
{
    private const string PLAYER_RECORDS_PREFS_KEY = "PlayerRecord";

    private const char RECORDS_DELIMITER = '-';
    private const char PLAYERS_DATA_DELIMITER = ':';

    private string PlayerRecords => PlayerPrefs.GetString(PLAYER_RECORDS_PREFS_KEY, string.Empty);

    public void SaveRecord(string playerName, int score)
    {
        PlayerPrefs.SetString(PLAYER_RECORDS_PREFS_KEY, $"{PlayerRecords}-{playerName}:{score}");
    }

    public List<KeyValuePair<string, int>> GetPlayersRecords()
    {
        List<KeyValuePair<string, int>> preparedRecords = new List<KeyValuePair<string, int>>();
        string records = PlayerRecords;
        bool hasRecords = !string.IsNullOrEmpty(records);
        if (hasRecords)
        {
            string[] splitRecords = records.Split(RECORDS_DELIMITER);

            foreach (string record in splitRecords)
            {
                if (!string.IsNullOrEmpty(record))
                {
                    string[] recordData = record.Split(PLAYERS_DATA_DELIMITER);
                    if (recordData.Length > 1)
                    {
                        preparedRecords.Add(new KeyValuePair<string, int>(recordData[0], int.Parse(recordData[1])));
                    }
                    else
                    {
                        Debug.LogError($"error reading record [{record}]");
                    }
                }
            }
        }
        List<KeyValuePair<string, int>> sortedRecords = preparedRecords.OrderByDescending(key => key.Value).ToList();
        return sortedRecords;
    }
}