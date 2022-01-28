using UnityEngine;

// Class to store data of placed furniture in the scene.
[System.Serializable]
public class PlacedFurniture
{
    public int furnitureId; // ID of furniture prefab
    public float[] position = new float[3]; // Furniture position
    public float yrotation; // Furniture y rotation
    public float[] color = new float[3]; // Furniture color

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
