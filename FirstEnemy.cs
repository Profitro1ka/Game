using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class FirstEnemy : Movable, IEnemy
{
    private Player _player;
    private float _attackCd;
    private float _toPlayerRange => (_player.Position - Position).Length();
    private float _attackRange;

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
        Hp = 100;
        PatternAttacks = new []{patternAttack};
        _attackRange = PatternAttacks[0].AttackRange;
        _attackCd = PatternAttacks[0].AttackCd;
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
        if (_ShootTimer > _attackCd)
        {
            if (CanShoot())
            {
                foreach (var patternAttack in PatternAttacks)
                {
                    _attackRange = patternAttack.AttackRange;
                    _attackCd = patternAttack.AttackCd;
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
        if (_toPlayerRange > 0.9 * _attackRange)
            Position += Speed * DirectionToTarget;
    }

    private bool CanShoot()
    {
        return _attackRange > _toPlayerRange ;
    }
}