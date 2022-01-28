using UnityEngine;
[System.Serializable]
public class WallData
{
    public IWallUnit[,] wallUnits;

    public WallData(int width, int length)
    {
        wallUnits = new IWallUnit[width, length];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                if (x != 0 && z != 0)
                {
                    wallUnits[x, z] = new WallUnit();
                } 
                else if (x == 0)
                {
                    wallUnits[x, z] = new LeftMostWallUnit();
                }
                else if (z == 0)
                {
                    wallUnits[x, z] = new BottomMostWallUnit();
                }
                else
                {
                    wallUnits[x, z] = new BottomLeftMostWallUnit();
                }
            }
        }
    }
}
