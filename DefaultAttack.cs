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
    
    public void Pattern(List<Sprite> sprites, Bullet bullet, Sprite parent, Vector2 directionToTarget,Vector2 position)
    {
        AddBullet(sprites, bullet, parent, directionToTarget, position);
    }

    public Texture2D Texture { get; }

    private void AddBullet(List<Sprite> sprites, Bullet bullet, Sprite parent, Vector2 directionToTarget,Vector2 position)
    {
        var thisBullet = bullet.Clone() as Bullet;
        thisBullet.Position = position;
        thisBullet.LifeSpan = 2f;
        thisBullet.Speed = 10f;
        thisBullet.Parent = parent;
        thisBullet.Damage = 10f;
        thisBullet.DirectionToTarget = directionToTarget;
        
        sprites.Add(thisBullet);
    }
}