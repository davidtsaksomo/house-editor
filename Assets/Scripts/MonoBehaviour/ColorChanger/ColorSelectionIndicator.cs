using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelectionIndicator : MonoBehaviour
{
    // Singleton
    public static ColorSelectionIndicator instance;

    [SerializeField]
    GameObject colorIndicator = null;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void SetColor(Color color)
    {
        colorIndicator.GetComponent<Image>().color = color;
    }

    public void Hide()
    {
        colorIndicator.SetActive(false);
    }

    public void Show()
    {
        colorIndicator.SetActive(true);
    }
}
