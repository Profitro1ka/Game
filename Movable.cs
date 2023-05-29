using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Movable: Sprite
{
    public float Speed = 5;
    
    //public float BulletSpeed = 10;

    public Movable(Texture2D texture2D) : base(texture2D)
    {
    }

    public Movable(Texture2D texture2D, Vector2 position) : base(texture2D, position)
    {
    }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        Move();
    }

    protected virtual void SearchTarget()
    {
        
    }

    protected virtual void Move()
    {
        
    }
}