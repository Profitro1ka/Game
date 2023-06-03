using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Movable: Sprite, IShooter
{
    public float Speed = 5;
    public float Hp;
    
    protected float _ShootTimer;
    protected Vector2 Target; 
    public Vector2 DirectionToTarget;
    
    protected Movable(Texture2D texture2D) : base(texture2D)
    {
    }

    protected Movable(Texture2D texture2D, Vector2 position) : base(texture2D, position)
    {
    }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        Move();
        SearchTarget();
        Shoot(sprites, gameTime);
        IsDead();
    }

    protected virtual void SearchTarget()
    {
        
    }

    protected virtual void Move()
    {
    }

    public float AttackCd { get ; set ; }

    public float AttackRange { get; }

    public Bullet CurBullet { get; set; }

    public virtual void Shoot(List<Sprite> sprites, GameTime gameTime)
    {
    }

    protected void ShooterTimer(GameTime gameTime)
    {
        _ShootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    protected void IsDead()
    {
        if (Hp <= 0)
            IsRemoved = true;
    }
}