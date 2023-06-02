using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class RoundAttack: IPatternAttack
{
    public RoundAttack(Texture2D texture)
    {
        Texture = texture;
    }
    
    public void Pattern(List<Sprite> sprites, Bullet bullet, Sprite parent, Vector2 directionToTarget,Vector2 position)
    {
        for (double i = 0; i < Math.PI * 2; i+= Math.PI/8)
        {
            var x = (float)Math.Cos(i);
            var y = (float)Math.Sin(i);
            
            AddBullet(sprites, bullet, parent, new Vector2(x, y), position);
        }
    }

    public Texture2D Texture { get; }

    private void AddBullet(List<Sprite> sprites, Bullet bullet, Sprite parent, Vector2 directionToTarget,Vector2 position)
    {
        var thisBullet = bullet.Clone() as Bullet;
        thisBullet.Position = position;
        thisBullet.LifeSpan = 2f;
        thisBullet.Speed = 5f;
        thisBullet.Parent = parent;
        thisBullet.Damage = 15f;
        thisBullet.DirectionToTarget = directionToTarget;
        
        sprites.Add(thisBullet);
    }
}