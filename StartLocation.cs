using System.IO;
using Microsoft.Xna.Framework;

namespace MyGame;

public class StartLocation: ILocation
{
    public string Map { get; set; }
    public Rectangle Enter { get; set; }
    public Rectangle Exit { get; set; }
    public Rectangle Bound { get; set; }
    public bool IsCreateNext { get; set; }

    public StartLocation()
    {
        Parse();
    }
    
    public void Parse()
    {
        Map = File.ReadAllText("startLoc.txt");
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}