
public interface IWallUnit
{
    Wall Top { get; set; }
    Wall Right { get; set; }
    Wall Bottom { get; set; }
    Wall Left { get; set; }
}

// Normal wall unit can only have top and right wall.
[System.Serializable]
public class WallUnit : IWallUnit
{
    Wall _top = new Wall();
    Wall _right = new Wall();

    Wall emptyWall = new Wall();

    public Wall Top
    {
        get { return _top; }
        set { _top = value; }
    }

    public Wall Right
    {
        get { return _right; }
        set { _right = value; }
    }

    public virtual Wall Bottom
    {
        get { return emptyWall; }
        set { }
    }

    public virtual Wall Left
    {
        get { return emptyWall; }
        set { }
    }
}

// Bottom most wall unit can have bottom wall in addition to top and right wall
[System.Serializable]
public class BottomMostWallUnit : WallUnit
{
    Wall _bottom = new Wall();

    public override Wall Bottom
    {
        get { return _bottom; }
        set { _bottom = value; }
    }
}

// Left most wall unit can have left wall in addition to top and right wall
[System.Serializable]
public class LeftMostWallUnit : WallUnit
{
    Wall _left = new Wall();

    public override Wall Left
    {
        get { return _left; }
        set { _left = value; }
    }
}

// Bottomleft most wall unit can have wall on all 4 sides.
[System.Serializable]
public class BottomLeftMostWallUnit : BottomMostWallUnit
{
    Wall _left = new Wall();

    public override Wall Left
    {
        get { return _left; }
        set { _left = value; }
    }
}