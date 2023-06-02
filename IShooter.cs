using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyGame;

public interface IShooter
{
    public float AttackCd { get; set; }
    Bullet CurBullet { get; set; }

    void Shoot(List<Sprite> sprites, GameTime gameTime);
}