using UnityEngine;

[System.Serializable]
public class Wall
{
    public bool exist = false;
    public float[] color = new float[3];
    public IWallProp wallProp;
}
