
public interface IWallUnit
{
    bool Top { get; set; }
    bool Right { get; set; }
    bool Bottom { get; set; }
    bool Left { get; set; }
}

// Normal wall unit can only have right and bottom wall.
public class WallUnit: IWallUnit
{
    bool _right;
    bool _bottom;

    public virtual bool Top
    {
        get { return false; }
        set { }
    }

    public bool Right
    {
        get { return _right; }
        set { _right = value; }
    }

    public bool Bottom
    {
        get { return _bottom; }
        set { _bottom = value; }
    }

    public virtual bool Left
    {
        get { return false; }
        set { }
    }
}

// Top most wall unit can have top wall in addition to right and bottom wall
public class TopMostWallUnit : WallUnit
{
    bool _top;

    public override bool Top
    {
        get { return _top; }
        set { _top = value; }
    }
}

// Left most wall unit can have left wall in addition to right and bottom wall
public class LeftMostWallUnit: WallUnit
{
    bool _left;

    public override bool Left
    {
        get { return _left; }
        set { _left = value; }
    }
}

// Topleft most wall unit can have wall on all 4 sides.
public class TopLeftMostWallUnit: TopMostWallUnit
{
    bool _left;

    public override bool Left
    {
        get { return _left; }
        set { _left = value; }
    }
}