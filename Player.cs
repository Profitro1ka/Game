using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Player : Movable//, IShooter
{
    private int _dashRange = 200;
    
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
        Dash(gameTime);
        Move();
        Shoot(sprites, gameTime);
        IsDead();
    }

    protected override void Move()
    {
        var nextMove = Position;
        
        if (_currentKey.IsKeyDown(Keys.A))
            nextMove.X -= Speed;
        
        if (_currentKey.IsKeyDown(Keys.D))
            nextMove.X += Speed;
        
        if (_currentKey.IsKeyDown(Keys.S))
            nextMove.Y += Speed;
        
        if (_currentKey.IsKeyDown(Keys.W))
            nextMove.Y -= Speed;
        
        var nextBounds = new Rectangle((int)nextMove.X, (int)nextMove.Y, _texture2D.Width, _texture2D.Height);

        foreach (var otherSprite in Game1.Sprites)
        {
            if (otherSprite != this && otherSprite is not Bullet)
            {
                if (nextBounds.Intersects(otherSprite.Bounds))
                {
                    return;
                }
            }
        }

        Position = nextMove;
    } 
    
    protected override void SearchTarget()
    {
        Target = Game1.Cursor;

        DirectionToTarget = Target - Position;
        DirectionToTarget.Normalize();
        Rotation = (float)Math.Atan2(DirectionToTarget.Y, DirectionToTarget.X);
    }

    private Vector2 _dashStartPosition;
    private Vector2 _dashEndPosition; // добавлено поле для сохранения конечной позиции DASH
    private const float DashDuration = 0.2f;
    public bool IsDashing;
    private TimeSpan _dashEndTime;
    

    private void Dash(GameTime gameTime)
    {
        if (_currentKey.IsKeyDown(Keys.Space) && _currentKey != _previousKey)
        {
            _dashStartPosition = Position;
            _dashEndPosition = Position + _dashRange * DirectionToTarget; 
            
            var nextBounds = new Rectangle((int)_dashEndPosition.X, (int)_dashEndPosition.Y, _texture2D.Width, _texture2D.Height);

            foreach (var otherSprite in Game1.Sprites)
            {
                if (otherSprite != this && otherSprite is not Bullet)
                {
                    if (nextBounds.Intersects(otherSprite.Bounds))
                    {
                        IsDashing = false;
                        return;
                    }
                }
            }
            
            _dashEndTime = gameTime.TotalGameTime + TimeSpan.FromSeconds(DashDuration);
            IsDashing = true;
        }

        if (IsDashing)
        {
            var timeFraction = (float)(_dashEndTime - gameTime.TotalGameTime).TotalSeconds / DashDuration;
            var distanceFraction = 1 - timeFraction;

            Position = _dashStartPosition + distanceFraction * (_dashEndPosition - _dashStartPosition);

            if (timeFraction <= 0)
            {
                IsDashing = false;
                Position = _dashEndPosition; 
            }
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