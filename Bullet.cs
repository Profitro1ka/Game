using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Bullet: Movable
{
    private float _timer;
    
    public float LifeSpan = 3f;
    public new float Speed = 10f; // костыль, надо исправить
    
    public Bullet(Texture2D texture2D) : base(texture2D)
    {
    }

    public Bullet(Texture2D texture2D, Vector2 position) : base(texture2D, position)
    {
    }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer > LifeSpan)
            IsRemoved = true;

        Position += DirectionToTarget * Speed;
    }
}