using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class DefaultAttack : IPatternAttack
{
    public DefaultAttack(Texture2D texture)
    {
        Texture = texture;
    }

    public float AttackRange => 500f;
    public float AttackCd => 1f;


    public void Pattern(List<Sprite> sprites, Bullet bullet, Sprite parent, Vector2 directionToTarget,Vector2 position)
    {
        AddBullet(sprites, bullet, parent, directionToTarget, position);
    }

    public Texture2D Texture { get; }

    private void AddBullet(List<Sprite> sprites, Bullet bullet, Sprite parent, Vector2 directionToTarget,Vector2 position)
    {
        var thisBullet = bullet.Clone() as Bullet;
        thisBullet.Position = position;
        thisBullet.LifeSpan = 5f;
        thisBullet.Speed = 10f;
        thisBullet.Parent = parent;
        thisBullet.Damage = 10f;
        thisBullet.DirectionToTarget = directionToTarget;
        
        sprites.Add(thisBullet);
    }
}

public class BossPattern : IPatternAttack
{
    public BossPattern(Texture2D texture)
    {
        Texture = texture;
    }

    public float AttackRange => 500f;
    public float AttackCd => 0.8f;

    public void Pattern(List<Sprite> sprites, Bullet bullet, Sprite parent, Vector2 directionToTarget,Vector2 position)
    {
        for (double i = 0; i < Math.PI; i+= Math.PI/16)
        {
            var x = (float)Math.Cos(i);
            var y = (float)Math.Sin(i);
            
            AddBullet(sprites, bullet, parent, new Vector2(x, y)* directionToTarget , position);
        }
    }

    public Texture2D Texture { get; }

    private void AddBullet(List<Sprite> sprites, Bullet bullet, Sprite parent, Vector2 directionToTarget,Vector2 position)
    {
        var thisBullet = bullet.Clone() as Bullet;
        thisBullet.Position = position;
        thisBullet.LifeSpan = 5f;
        thisBullet.Speed = 5f;
        thisBullet.Parent = parent;
        thisBullet.Damage = 10f;
        thisBullet.DirectionToTarget = directionToTarget;
        
        sprites.Add(thisBullet);
    }
}