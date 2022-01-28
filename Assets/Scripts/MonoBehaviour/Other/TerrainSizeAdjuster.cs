using UnityEngine;

public class TerrainSizeAdjuster : MonoBehaviour
{
    [SerializeField]
    GameObject terrain = null;

    private void Start()
    {
        TerrainData terrainData = terrain.GetComponent<Terrain>().terrainData;
        terrainData.size = new Vector3(GameConstants.worldWidth, terrainData.size.y, GameConstants.worldLength);
    }
}
