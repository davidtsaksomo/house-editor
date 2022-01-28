using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Singleton
    public static ColorChanger instance;

    int selectedColorIndex = 0;

    [SerializeField]
    Color[] colors = null;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        ColorSelectionIndicator.instance.SetColor(colors[selectedColorIndex]);
    }

    public void ChangeObjectColor(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitData, 1000, 1 << LayerMask.NameToLayer("Furniture") | 1 << LayerMask.NameToLayer(GameConstants.wallName)))
        {
            MeshRenderer meshRenderer = hitData.collider.gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer)
            {
                meshRenderer.material.color = colors[selectedColorIndex];
            }
        }
    }

    public void ChangeColorSelection()
    {
        selectedColorIndex = (selectedColorIndex + 1) % colors.Length;
        ColorSelectionIndicator.instance.SetColor(colors[selectedColorIndex]);
    }
}
