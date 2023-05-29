using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Sprite : ICloneable
{
    protected Texture2D _texture2D;
    
    protected float _rotation;

    protected Vector2 _target; // вынести в отдельный интерфейс
    public Vector2 DirectionToTarget;
    
    public Vector2 Position;
    public Vector2 Origin;

    public bool IsRemoved = false;
    public Sprite Parent;
    
    //public float RotationVelocity = 3f;

    public Sprite(Texture2D texture2D)
    {
        _texture2D = texture2D;
        Origin = new Vector2(_texture2D.Width / 2f, _texture2D.Height / 2f);
    }

    public Sprite(Texture2D texture2D, Vector2 position)
    {
        Position = position;
        _texture2D = texture2D;
        Origin = new Vector2(_texture2D.Width / 2f, _texture2D.Height / 2f);
    }

    public virtual void Update(GameTime gameTime, List<Sprite> sprites)
    {
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture2D, Position, null,  Color.White, _rotation, Origin, 1f, SpriteEffects.None, 0f);
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}