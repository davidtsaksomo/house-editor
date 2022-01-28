using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Manage information text object
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
        textField = GetComponent<TextMeshProUGUI>();
    }

    public void setText(string message)
    {
        textField.text = message;
    }
}
