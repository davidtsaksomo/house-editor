
public interface IWallPropUnit
{
    IWallProp Top { get; set; }
    IWallProp Right { get; set; }
    IWallProp Bottom { get; set; }
    IWallProp Left { get; set; }
}

// Normal wall unit can only have top and right wall.
public class WallPropUnit : IWallPropUnit
{
    IWallProp _top;
    IWallProp _right;

    public IWallProp Top
    {
        get { return _top; }
        set { _top = value; }
    }

    public IWallProp Right
    {
        get { return _right; }
        set { _right = value; }
    }

    public virtual IWallProp Bottom
    {
        get { return null; }
        set { }
    }

    public virtual IWallProp Left
    {
        get { return null; }
        set { }
    }
}

// Bottom most wall unit can have bottom wall in addition to top and right wall
public class BottomMostWallPropUnit : WallPropUnit
{
    IWallProp _bottom;

    public override IWallProp Bottom
    {
        get { return _bottom; }
        set { _bottom = value; }
    }
}

// Left most wall unit can have left wall in addition to top and right wall
public class LeftMostWallPropUnit : WallPropUnit
{
    IWallProp _left;

    public override IWallProp Left
    {
        get { return _left; }
        set { _left = value; }
    }
}

// Bottomleft most wall unit can have wall on all 4 sides.
public class BottomLeftMostWallPropUnit : BottomMostWallPropUnit
{
    IWallProp _left;

    public override IWallProp Left
    {
        get { return _left; }
        set { _left = value; }
    }
}