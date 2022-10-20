using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class PreserveToggleStatus : MonoBehaviour
{
    private bool SavedStatus
    {
        get => Convert.ToBoolean(PlayerPrefs.GetInt(togglePrefsKey, Convert.ToInt32(defaultStatus)));
        set
        {
            PlayerPrefs.SetInt(togglePrefsKey, Convert.ToInt32(value));
            PlayerPrefs.Save();
        }
    }

    private Toggle toggle;

    [SerializeField]
    private string togglePrefsKey;

    [SerializeField]
    private bool defaultStatus;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        toggle.SetIsOnWithoutNotify(SavedStatus);
        toggle.onValueChanged.Invoke(SavedStatus);
        toggle.onValueChanged.AddListener(SetStatusFromToggle);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(SetStatusFromToggle);
    }

    private void SetStatusFromToggle(bool isOn)
    {
        SavedStatus = isOn;
    }
}
