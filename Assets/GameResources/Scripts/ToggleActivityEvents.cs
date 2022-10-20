using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleActivityEvents : MonoBehaviour
{
    private Toggle toggle;

    [SerializeField]
    private UnityEvent activatedEvent;
    [SerializeField]
    private UnityEvent deactivatedEvent;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleStatusChanged);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.AddListener(OnToggleStatusChanged);
    }

    private void OnToggleStatusChanged(bool isOn)
    {
        if (isOn)
            activatedEvent.Invoke();
        else
            deactivatedEvent.Invoke();
    }
}
