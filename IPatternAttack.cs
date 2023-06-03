using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public interface IPatternAttack
{
    public float AttackRange { get;}
    public float AttackCd { get; }
    public void Pattern(List<Sprite> sprites, Bullet bullet, Sprite parent, Vector2 directionToTarget,Vector2 position);
    public Texture2D Texture {get;}
}