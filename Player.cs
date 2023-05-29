using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Player : Movable, IShooter
{
    public int Hp;
    public int AttackCD;
    public int DashRange = 100;
    public int DashCDTime = 100;
    
    private int _dashCD;
    private KeyboardState _currentKey;
    private KeyboardState _previousKey;
    public Bullet Bullet { get; set; }
    
    public Player(Texture2D texture2D) : base(texture2D)
    {
    }
    
    public Player(Texture2D texture2D, Vector2 position) : base(texture2D, position)
    {
    }
    
    public Player(Texture2D texture2D, Vector2 position, Bullet bullet) : base(texture2D, position)
    {
        Bullet = bullet;
    }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        SearchTarget();
        _previousKey = _currentKey;
        _currentKey = Keyboard.GetState();
        Move();
        Dash();
        Shoot(sprites);
    }

    protected override void Move()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            Position.X -= Speed;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            Position.X += Speed;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            //Position += DirectionToTarget * Speed;
            Position.Y -= Speed;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            Position.Y += Speed;
        }
    }
    
    protected override void SearchTarget()
    {
        var mouseState = Mouse.GetState();
        _target = new Vector2(mouseState.X, mouseState.Y);
        DirectionToTarget = _target - Position;
        DirectionToTarget.Normalize();
        _rotation = (float)Math.Atan2(DirectionToTarget.Y, DirectionToTarget.X);
    }

    private void Dash()
    {
        if (_currentKey.IsKeyDown(Keys.Space) && _currentKey != _previousKey)
        {
            Position += DirectionToTarget * DashRange;

            //_dashCD = DashCDTime;
        }
        // else if(_dashCD - 1 >= 0 )
        //     _dashCD -= 1;
    }

    private void AddBullet(List<Sprite> sprites)
    {
        var bullet = Bullet.Clone() as Bullet;
        bullet.Position = Position;
        bullet.LifeSpan = 2f;
        bullet.Speed = 10f;
        bullet.Parent = this;
        bullet.DirectionToTarget = DirectionToTarget;
        
        sprites.Add(bullet);
    }

    public void Shoot(List<Sprite> sprites)
    {
        if (_currentKey.IsKeyDown(Keys.C) && _previousKey.IsKeyUp(Keys.C))
        {
            AddBullet(sprites);

        }
    }
}