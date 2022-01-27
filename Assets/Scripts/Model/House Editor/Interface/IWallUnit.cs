
public interface IWallUnit
{
    bool Top { get; set; }
    bool Right { get; set; }
    bool Bottom { get; set; }
    bool Left { get; set; }
}

// Normal wall unit can only have top and right wall.
public class WallUnit : IWallUnit
{
    bool _top;
    bool _right;

    public bool Top
    {
        get { return _top; }
        set { _top = value; }
    }

    public bool Right
    {
        get { return _right; }
        set { _right = value; }
    }

    public virtual bool Bottom
    {
        get { return false; }
        set { }
    }

    public virtual bool Left
    {
        get { return false; }
        set { }
    }
}

// Bottom most wall unit can have bottom wall in addition to top and right wall
public class BottomMostWallUnit : WallUnit
{
    bool _bottom;

    public override bool Bottom
    {
        get { return _bottom; }
        set { _bottom = value; }
    }
}

// Left most wall unit can have left wall in addition to top and right wall
public class LeftMostWallUnit : WallUnit
{
    bool _left;

    public override bool Left
    {
        get { return _left; }
        set { _left = value; }
    }
}

// Bottomleft most wall unit can have wall on all 4 sides.
public class BottomLeftMostWallUnit : BottomMostWallUnit
{
    bool _left;

    public override bool Left
    {
        get { return _left; }
        set { _left = value; }
    }
}