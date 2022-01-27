using UnityEngine;
public class WallData
{
    public Wall[,] walls;

    public WallData(int width, int length)
    {
        walls = new Wall[width, length];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                walls[x, z] = new Wall();
                if (x == 0 && z == 0)
                {
                    walls[x, z].wallUnit = new BottomLeftMostWallUnit();
                    walls[x, z].wallPropUnit = new BottomLeftMostWallPropUnit();
                } 
                else if (x == 0)
                {
                    walls[x, z].wallUnit = new LeftMostWallUnit();
                    walls[x, z].wallPropUnit = new BottomLeftMostWallPropUnit();
                }
                else if (z == 0)
                {
                    walls[x, z].wallUnit = new BottomMostWallUnit();
                    walls[x, z].wallPropUnit = new BottomLeftMostWallPropUnit();
                }
                else
                {
                    walls[x, z].wallUnit = new WallUnit();
                    walls[x, z].wallPropUnit = new BottomLeftMostWallPropUnit();
                }
            }
        }
    }
}
