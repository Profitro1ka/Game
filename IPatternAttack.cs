using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public interface IPatternAttack
{
    public void Pattern(List<Sprite> sprites, Bullet bullet, Sprite parent, Vector2 directionToTarget,Vector2 position);
    public Texture2D Texture {get;}
}