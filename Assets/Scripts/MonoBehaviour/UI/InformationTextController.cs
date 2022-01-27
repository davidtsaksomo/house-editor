using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformationTextController : MonoBehaviour
{
    // Singleton
    public static InformationTextController instance;

    TextMeshProUGUI textField;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }

    public void setText(string message)
    {
        textField.text = message;
    }
}
