using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Bullet: Movable
{
    private float _lifeTimer;
    public float LifeSpan ;
    public new float Speed ;
    public float Damage;
    
    public Bullet(Texture2D texture2D) : base(texture2D)
    {
    }

    public Bullet(Texture2D texture2D, Vector2 position) : base(texture2D, position)
    {
        
    }

    protected override bool Сollide()
    {
        foreach (var otherSprite in Game1.Sprites)
        {
            if (this != otherSprite && Parent != otherSprite &&  Bounds.Intersects(otherSprite.Bounds))
            {
                if (otherSprite is Wall)
                    IsRemoved = true;
                if(Parent is Enemy && otherSprite is Enemy || otherSprite is Player { IsDashing: true })
                    continue;
                if (otherSprite is not Bullet && otherSprite is Movable movable)
                {
                    movable.Hp -= Damage;
                    IsRemoved = true;
                }
            }
        }

        return true;
    }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        _lifeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        Сollide();
        if (_lifeTimer > LifeSpan)
            IsRemoved = true;
        else
            Position += DirectionToTarget * Speed;
    }
}