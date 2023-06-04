using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Sprite : ICloneable
{
    protected Texture2D _texture2D;

    public virtual Rectangle Bounds =>new((int)(Position.X - _texture2D.Width/2.7), 
        (int)(Position.Y - _texture2D.Height/2.7), _texture2D.Width, _texture2D.Height);

    protected float Rotation;

    public Vector2 Position;
    protected readonly Vector2 _origin;

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