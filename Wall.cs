using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Wall: Sprite
{
    public static List<Wall> Walls = new();
    public Wall(Texture2D texture2D) : base(texture2D)
    {
    }

    protected Wall(Texture2D texture2D, Vector2 position) : base(texture2D, position)
    {
    }

    public void AddWall(List<Sprite> sprites, Vector2 position, Wall wall)
    {
        var newWall = wall.Clone() as Wall;
        newWall.Position = position;
        Walls.Add(newWall);
        sprites.Add(newWall);
    }
}