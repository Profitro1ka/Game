using System.Numerics;

namespace MyGame;

public interface IEnemy 
{
    IPatternAttack[] PatternAttacks { get; set; }
    float AttackRange { get; }
}