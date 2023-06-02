using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Sprite : ICloneable
{
    private Texture2D _texture2D;

    public Rectangle Bounds => new ((int)Position.X, (int)Position.Y, _texture2D.Width, _texture2D.Height);

    protected float Rotation;

    public Vector2 Position;
    private Vector2 _origin;

    public bool IsRemoved = false;
    public Sprite Parent;

    protected Sprite(Texture2D texture2D)
    {
        _texture2D = texture2D;
        _origin = new Vector2(_texture2D.Width / 2f, _texture2D.Height / 2f);
    }

    protected Sprite(Texture2D texture2D, Vector2 position)
    {
        Position = position;
        _texture2D = texture2D;
        _origin = new Vector2(_texture2D.Width / 2f, _texture2D.Height / 2f);
    }

    public virtual void Update(GameTime gameTime, List<Sprite> sprites)
    {
    }

    protected virtual bool Сollide()
    {
        // foreach (var otherSprite in Game1.Sprites)
        // {
        //     if (this != otherSprite && this!= otherSprite.Parent && Bounds.Intersects(otherSprite.Bounds))
        //     {
        //         if (this is Movable movable1 && otherSprite is Bullet bullet1)
        //         {
        //             movable1.Hp -= bullet1.Damage;
        //             bullet1.IsRemoved = true;
        //         }
        //     }
        // }


        return false;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture2D, Position, null,  Color.White, Rotation, _origin, 1f, SpriteEffects.None, 0f);
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}