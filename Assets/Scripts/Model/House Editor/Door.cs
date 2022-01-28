[System.Serializable]
public class Door : IWallProp
{
    const string _name = GameConstants.doorName;
    public string Name
    {
        get => _name;
    }
}
