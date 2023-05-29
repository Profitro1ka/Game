using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Bullet: Movable
{
    public Bullet(Texture2D texture2D, int width, int height) : base(texture2D, width, height)
    {
    }

    public Bullet(Texture2D texture2D) : base(texture2D)
    {
    }

    public Bullet(Texture2D texture2D, Vector2 position) : base(texture2D, position)
    {
    }
    
    
}