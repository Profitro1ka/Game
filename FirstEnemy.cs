using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class FirstEnemy : Movable, IEnemy
{
    private Player _player;
    public float AttackCd { get; }
    public float AttackRange = 500f;
    private float _toPlayerRange => (_player.Position - Position).Length();

    public IPatternAttack[] PatternAttacks { get; set; } 
    
    protected FirstEnemy(Texture2D texture2D) : base(texture2D)
    {
    }

    protected FirstEnemy(Texture2D texture2D, Vector2 position) : base(texture2D, position)
    {
    }

    public FirstEnemy(Texture2D texture2D, Vector2 position, Player player, Bullet curBullet, IPatternAttack patternAttack) : base(texture2D, position)
    {
        _player = player;
        CurBullet = curBullet;
        AttackCd = 1f;
        Hp = 100;
        PatternAttacks = new []{patternAttack};
    }
    
    protected override void SearchTarget()
    {
        Target = _player.Position;
        DirectionToTarget = Target - Position;
        DirectionToTarget.Normalize();
        Rotation = (float)Math.Atan2(DirectionToTarget.Y, DirectionToTarget.X);
    }

    public override void Shoot(List<Sprite> sprites, GameTime gameTime)
    {
        if (_ShootTimer > AttackCd)
        {
            if (CanShoot())
            {
                foreach (var patternAttack in PatternAttacks)
                {
                    patternAttack.Pattern(sprites, CurBullet, this, DirectionToTarget, Position);
                }

                _ShootTimer = 0;
            }
            else
                Move();
        }
        
        else
            ShooterTimer(gameTime);
    }

    protected override void Move()
    {
        if (_toPlayerRange > 0.9 * AttackRange)
            Position += Speed * DirectionToTarget;
    }

    public bool CanShoot()
    {
        return AttackRange > _toPlayerRange ;
    }
}