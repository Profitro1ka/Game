using System;
using Microsoft.Xna.Framework;

namespace MyGame;

public interface ILocation : ICloneable
{
    public string Map { get; set; }
    public Rectangle Exit { get; set; }
    public Rectangle Enter { get; set; }
    public Rectangle Bound { get; set; }
    public bool IsCreateNext { get; set; }
    public void Parse();
}