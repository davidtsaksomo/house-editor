using UnityEngine;

[System.Serializable]
public class PlacedFurniture
{
    int instanceId;
    int furnitureIndex;
    float[] position = new float[3];
    float yrotation;
    float[] color = new float[3];

    public PlacedFurniture(int instanceId, int furnitureIndex, Transform transform, Color color)
    {
        this.instanceId = instanceId;
        this.furnitureIndex = furnitureIndex;
        position[0] = transform.position.x;
        position[1] = transform.position.y;
        position[2] = transform.position.z;
        yrotation = transform.eulerAngles.y;
        this.color[0] = color.r;
        this.color[1] = color.g;
        this.color[2] = color.b;
    }
}
