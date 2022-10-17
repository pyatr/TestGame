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

    [SerializeField]
    private GameObject noRecordsObject;

    private void OnEnable()
    {
        List<KeyValuePair<string, int>> records = recordController.GetPlayersRecords();
        noRecordsObject.SetActive(records.Count == 0);
        if (records.Count > 0)
        {
            foreach (KeyValuePair<string, int> record in records)
            {
                SingleRecordView singleRecordView = Instantiate(recordViewPrefab, contentTransform);
                singleRecordView.PlayerNameText = record.Key;
                singleRecordView.PlayerScoreText = record.Value.ToString();
                displayedRecords.Add(singleRecordView);
            }
        }
    }

    private void OnDisable()
    {
        displayedRecords.DestroyGameObjects();
        displayedRecords.Clear();
    }
}
