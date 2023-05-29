using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Player : Movable
{
    public int Hp;
    public int AttackCD;
    public int DashRange = 100;
    public int DashCDTime = 100;
    private Direction _direction;
    private int _dashCD;
    

    public Player(Texture2D texture2D, int width, int height) : base(texture2D, width, height)
    {
    }

    public Player(Texture2D texture2D) : base(texture2D)
    {
    }

    public Player(Texture2D texture2D, Vector2 position) : base(texture2D, position)
    {
    }

    public void Update()
    {
        Move();
        Dash();
    }

    protected override void Move()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            Position.X -= Speed;
            _direction = Direction.Left;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            Position.X += Speed;
            _direction = Direction.Right;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            Position.Y -= Speed;
            _direction = Direction.Up;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            Position.Y += Speed;
            _direction = Direction.Down;
        }
    }

    enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }
    
    private void Dash()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Space) && _dashCD <= 0)
        {
            switch (_direction)
            {
                case Direction.Down:
                    Position.Y += DashRange;
                    break;
                case Direction.Up:
                    Position.Y -= DashRange;
                    break;
                case Direction.Left:
                    Position.X -= DashRange;
                    break;
                case Direction.Right:
                    Position.X += DashRange;
                    break;
            }

            _dashCD = DashCDTime;
        }
        
        else if(_dashCD - 1 >= 0 )
            _dashCD -= 1;
    }
        
    
    private void Shoot()
    {
        
    }
}