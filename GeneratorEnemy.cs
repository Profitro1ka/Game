using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class GeneratorEnemy
{
    private IPatternAttack[] _typeOfEnemy;
    private Random _random = new();
    private List<Sprite> _sprites;
    private Player _player;
    private readonly Bullet _bullet;
    private float _timer;
    private float _spawnCD;

    public GeneratorEnemy(IPatternAttack[] typeOfEnemy, List<Sprite> sprites, Player player, Bullet bullet)
    {
        _typeOfEnemy = typeOfEnemy;
        _player = player;
        _sprites = sprites;
        _bullet = bullet;
    }

    private Vector2 GetRandomPosition()
    {
        while (true)
        {
            var x = _random.Next((int)(_player.Position.X - Game1.ScreenWight), (int)(_player.Position.X + Game1.ScreenWight));
            var y = _random.Next(2) == 0 ? -50 : Game1.ScreenHeight + 50;

            if (Math.Sqrt(Math.Pow(x - _player.Position.X, 2) + Math.Pow(y - _player.Position.Y, 2)) < 600) continue;

            return new Vector2(x, y);
        }
    }

    private FirstEnemy CreateRandomEnemy()
    {
        var index = _random.Next(0, _typeOfEnemy.Length);
        var texture = _typeOfEnemy[index].Texture;
        return new FirstEnemy(texture, GetRandomPosition(),_player, _bullet, _typeOfEnemy[index]);
    }

    public void AddRandomEnemy(GameTime gameTime)
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if(_timer > _spawnCD)
        {
            _spawnCD = _random.Next(5, 15);
            _sprites.Add(CreateRandomEnemy());
            _timer = 0;
        }
    }
}