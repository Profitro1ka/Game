using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Movable: Sprite
{
    public float Speed = 5;
    
    public Movable(Texture2D texture2D, int width, int height) : base(texture2D, width, height)
    {
    }

    public Movable(Texture2D texture2D) : base(texture2D)
    {
    }

    public Movable(Texture2D texture2D, Vector2 position) : base(texture2D, position)
    {
    }

    public virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        
    }
}