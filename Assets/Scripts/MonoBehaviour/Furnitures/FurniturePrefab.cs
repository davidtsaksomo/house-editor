using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePrefab : MonoBehaviour
{
    [HideInInspector]
    public bool placeable = true;

    MeshRenderer meshRenderer;

    float invokeTime = 0.1f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Furniture")) && placeable)
        {
            SetNotPlaceable();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Furniture")) && placeable)
        {
            SetNotPlaceable();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Furniture"))  && !placeable)
        {
            Invoke("SetPlaceable", invokeTime);
        }
    }

    void SetNotPlaceable()
    {
        placeable = false;
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 0.3f);
        CancelInvoke();
    }

    void SetPlaceable()
    {
        placeable = true;
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 1f);
    }
}
