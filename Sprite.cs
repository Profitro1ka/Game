using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Sprite
{
    protected Texture2D _texture2D;
    protected int _spriteHeight;
    protected int _spritewidth;
    
    public Vector2 Position;

    public Sprite(Texture2D texture2D, int width, int height)
    {
        _texture2D = texture2D;
        _spriteHeight = height;
        _spritewidth = width;
    }
     
    public Sprite(Texture2D texture2D)
    {
        _texture2D = texture2D;
        _spriteHeight = texture2D.Height;
        _spritewidth = texture2D.Width;
    }

    public Sprite(Texture2D texture2D, Vector2 position)
    {
        Position = position;
        _texture2D = texture2D;
        _spriteHeight = texture2D.Height;
        _spritewidth = texture2D.Width;
    }

    // public void Update()
    // {
    //     
    // }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture2D, new Rectangle((int)Position.X,(int)Position.Y, _spritewidth, _spriteHeight), Color.White);
    }
}