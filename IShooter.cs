using System.Collections.Generic;

namespace MyGame;

public interface IShooter
{
    Bullet Bullet { get; set; }

    public void Shoot(List<Sprite> sprites);
    private void AddBullet(List<Sprite> sprites)
    {
        throw new System.NotImplementedException();
    }
}