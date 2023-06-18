using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyGame;

public class LocationGenerator
{
    private ILocation _location;
    private int _spriteSize;
    public static List<ILocation> CreatedLocation = new(); 

    public LocationGenerator(ILocation location, Wall wall, List<Sprite> sprites, Vector2 position)
    {
        _location = location;
        _spriteSize = wall.Bounds.Height;
        GenerateLocation(wall, sprites, position );
    }
    
    private void GenerateLocation(Wall wall, List<Sprite> sprites, Vector2 position)
    {
        var rows = _location.Map.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

        var delta = position - SearchEnter(rows, position);
        
        for (var i = 0; i < rows[0].Length; i++)
        for (var j = 0; j < rows.Length; j++)
        {
            var x =  (int)(position.X + i * _spriteSize + delta.X);
            var y =  (int)(position.Y + j * _spriteSize + delta.Y);

            switch (rows[j][i])
            {
                case '#':
                    wall.AddWall(sprites, new Vector2(x , y), wall);
                    break;
                case 'e': 
                    _location.Exit = new Rectangle(new Point(x,y), new Point(x+_spriteSize, y+_spriteSize));
                    break;
                case 'E':
                    _location.Enter = new Rectangle(new Point(x,y), new Point(x+_spriteSize, y+_spriteSize));
                    break;
            }
        }

        CreatedLocation.Add(_location);
    }

    private Vector2 SearchEnter(string[] rows, Vector2 position)
    {
        for (var i = 0; i < rows[0].Length; i++)
        for (var j = 0; j < rows.Length; j++)
        {
            var x = (int)position.X + i * _spriteSize;
            var y = (int)position.Y + j * _spriteSize;

            if (rows[j][i] == 'E')
                return new Vector2(x, y);
        }

        return Vector2.Zero;
    }
}