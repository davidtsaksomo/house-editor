using UnityEngine;

[System.Serializable]
public class PlacedFurniture
{
    int furnitureId;
    float[] position = new float[3];
    float yrotation;
    float[] color = new float[3];

    public PlacedFurniture(int furnitureId, Transform transform, Color color)
    {
        this.furnitureId = furnitureId;
        position[0] = transform.position.x;
        position[1] = transform.position.y;
        position[2] = transform.position.z;
        yrotation = transform.eulerAngles.y;
        this.color[0] = color.r;
        this.color[1] = color.g;
        this.color[2] = color.b;
    }

    public void ChangeColor(Color color)
    {
        this.color[0] = color.r;
        this.color[1] = color.g;
        this.color[2] = color.b;
    }
}
