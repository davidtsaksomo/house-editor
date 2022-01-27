using UnityEngine;

[System.Serializable]
public class Wall
{
    public bool exist = false;
    public Color color = Color.white;
    public IWallProp wallProp;
}
