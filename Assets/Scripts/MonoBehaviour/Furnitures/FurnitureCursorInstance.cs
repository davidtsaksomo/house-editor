using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the furniture indicator on the mouse cursor
public class FurnitureCursorInstance : MonoBehaviour
{
    // Is furniture placeable
    [HideInInspector]
    public bool placeable = true;

    MeshRenderer meshRenderer;

    // Delay to switch from not placeable to placeable. To mitigate time difference between OnCollisionStay call and OnCollissionExit.
    float invokeTime = 0.1f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.CompareTag(GameConstants.wallName) || collision.gameObject.CompareTag(GameConstants.furnitureName))) // Object not placeable
        {
            CancelInvoke();
            if(placeable)
            {
                SetNotPlaceable();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag(GameConstants.wallName) || collision.gameObject.CompareTag(GameConstants.furnitureName))) // Object not placeable
        {
            CancelInvoke();
            if (placeable)
            {
                SetNotPlaceable();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((collision.gameObject.CompareTag(GameConstants.wallName) || collision.gameObject.CompareTag(GameConstants.furnitureName))  && !placeable) // Object Placeable
        {
            Invoke("SetPlaceable", invokeTime);
        }
    }

    void SetNotPlaceable()
    {
        placeable = false;
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 0.3f);
    }

    void SetPlaceable()
    {
        placeable = true;
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 1f);
    }
}
