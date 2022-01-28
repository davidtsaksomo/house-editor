using UnityEngine;

// Class to store information of a wall
[System.Serializable]
public class Wall
{
    public bool exist;
    public float[] color;
    public IWallProp wallProp;

    public Wall()
    {
        exist = false;
        color = new float[3];
        SetColor(GameConstants.DefaultWallColor);
    }

    public void SetColor(Color color)
    {
        this.color[0] = color.r;
        this.color[1] = color.g;
        this.color[2] = color.b;
    }
}
