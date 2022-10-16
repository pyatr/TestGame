using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordView : MonoBehaviour
{
    [SerializeField] private RecordController recordController;
    [SerializeField] private SingleRecordView recordViewPrefab;
    [SerializeField] private Transform contentTransform;

    private List<SingleRecordView> displayedRecords = new List<SingleRecordView>();

    private void OnEnable()
    {
        List<KeyValuePair<string, string>> records = recordController.GetPlayersRecords();
        if (records.Count > 0)
        {
            foreach (KeyValuePair<string, string> record in records)
            {
                SingleRecordView singleRecordView = Instantiate(recordViewPrefab, contentTransform);
                singleRecordView.PlayerNameText = record.Key;
                singleRecordView.PlayerScoreText = record.Value;
            }
        }
    }

    private void OnDisable()
    {
        displayedRecords.DestroyGameObjects();
        /*
        int recordCount = displayedRecords.Count;
        for (int i = 0; i < recordCount; i++)
        {
            Destroy(displayedRecords[i]);
        }
        */
    }
}
