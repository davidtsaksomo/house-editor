using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Render grid lines
public class GridRenderer : MonoBehaviour
{
    const float gridLineThickness = 0.05f;

    [Tooltip("Parent object to instantiate grid line objects to.")]
    [SerializeField]
    GameObject gridLineParent = null;
    [Tooltip("Grid line object to instantiate.")]
    [SerializeField]
    GameObject gridLinePrefab = null;

    void Start()
    {
        for (int x = 0; x <= GameConstants.worldWidth; x++)
        {
            GameObject gridLine = Instantiate(gridLinePrefab, new Vector3(x, 0.01f, (float)GameConstants.worldLength / 2), Quaternion.identity, gridLineParent.transform);
            gridLine.transform.localScale = new Vector3(gridLineThickness / GameConstants.planeScaleMultiplier, 1, (float)GameConstants.worldLength / 10);
        }

        for (int z = 0; z <= GameConstants.worldLength; z++)
        {
            GameObject gridLine = Instantiate(gridLinePrefab, new Vector3((float)GameConstants.worldWidth / 2, 0.01f, z), Quaternion.identity, gridLineParent.transform);
            gridLine.transform.localScale = new Vector3((float)GameConstants.worldWidth / 10, 1, gridLineThickness / GameConstants.planeScaleMultiplier);
        }
    }
}
