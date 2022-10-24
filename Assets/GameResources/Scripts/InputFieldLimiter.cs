using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class InputFieldLimiter : MonoBehaviour
{
    [SerializeField]
    private int characterLimit = 24;

    private InputField input;

    private void Awake()
    {
        input = GetComponent<InputField>();
    }

    private void OnEnable()
    {
        input.onValueChanged.AddListener(CutChars);
        input.onEndEdit.AddListener(CutChars);
        input.onSubmit.AddListener(CutChars);
    }

    private void OnDisable()
    {
        input.onValueChanged.AddListener(CutChars);
        input.onEndEdit.AddListener(CutChars);
        input.onSubmit.AddListener(CutChars);
    }

    private void CutChars(string newText)
    {
        if (newText.Length > characterLimit)
        {
            input.text = newText.Substring(0, characterLimit);
        }
    }
}
