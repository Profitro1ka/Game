using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Player : Movable//, IShooter
{
    private int _dashRange = 100;
    
    private KeyboardState _currentKey;
    private KeyboardState _previousKey;
    private float _attackCd { get ; }
    public new float AttackRange => CurBullet.LifeSpan * CurBullet.Speed;
    
    public Player(Texture2D texture2D) : base(texture2D)
    {
    }
    
    public Player(Texture2D texture2D, Vector2 position) : base(texture2D, position)
    {
    }
    
    public Player(Texture2D texture2D, Vector2 position, Bullet curBullet) : base(texture2D, position)
    {
        CurBullet = curBullet;
        _attackCd = 0.5f;
        Hp = 100;
    }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        SearchTarget();
        _previousKey = _currentKey;
        _currentKey = Keyboard.GetState();
        Move();
        Dash();
        Shoot(sprites, gameTime);
        IsDead();
    }
    
    protected override void Move()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.A))
            Position.X -= Speed;

        if (Keyboard.GetState().IsKeyDown(Keys.D))
            Position.X += Speed;

        if (Keyboard.GetState().IsKeyDown(Keys.W))
            Position.Y -= Speed;

        if (Keyboard.GetState().IsKeyDown(Keys.S))
            Position.Y += Speed;
    }
    
    protected override void SearchTarget()
    {
        Target = Game1.Cursor;

        DirectionToTarget = Target - Position;
        DirectionToTarget.Normalize();
        Rotation = (float)Math.Atan2(DirectionToTarget.Y, DirectionToTarget.X);
    }

    private void Dash()
    {
        if (_currentKey.IsKeyDown(Keys.Space) && _currentKey != _previousKey)
        {
            Position += DirectionToTarget * _dashRange;
        }
    }

    public override void Shoot(List<Sprite> sprites, GameTime gameTime)
    {
        if (_currentKey.IsKeyDown(Keys.C) && _previousKey.IsKeyUp(Keys.C) && _ShootTimer > _attackCd)
        {
            AddBullet(sprites);
            _ShootTimer = 0;
        }
        else
            ShooterTimer(gameTime);
    }
    
    private void AddBullet(List<Sprite> sprites)
    {
        var bullet = CurBullet.Clone() as Bullet;
        bullet.Position = Position;
        bullet.LifeSpan = 2f;
        bullet.Speed = 10f;
        bullet.Parent = this;
        bullet.Damage = 10f;
        bullet.DirectionToTarget = DirectionToTarget;
        
        sprites.Add(bullet);
    }
}