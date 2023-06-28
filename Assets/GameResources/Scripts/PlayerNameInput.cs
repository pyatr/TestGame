using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField]
    private InputField nameInput;

    private void Awake()
    {
        nameInput.text = PlayerProfile.PlayerName;
    }

    private void OnEnable()
    {
        nameInput.onValueChanged.AddListener(SetPlayerName);
        nameInput.onEndEdit.AddListener(SetPlayerName);
        nameInput.onSubmit.AddListener(SetPlayerName);
    }

    private void OnDisable()
    {
        nameInput.onValueChanged.RemoveListener(SetPlayerName);
        nameInput.onEndEdit.RemoveListener(SetPlayerName);
        nameInput.onSubmit.RemoveListener(SetPlayerName);
    }

    private void SetPlayerName(string newName)
    {
        PlayerProfile.PlayerName = newName;
    }
}
