using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AbstractTextView : MonoBehaviour
{
    protected Text viewText;

    protected virtual void Awake()
    {
        viewText = GetComponent<Text>();
    }
}
