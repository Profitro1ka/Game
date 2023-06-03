using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace MyGame;

public class LabyrinthGenerator
{
    private ILocation[] _locations;
    private Wall _wall;
    private List<Sprite> _sprites;
    private Random _random = new ();

    public LabyrinthGenerator(Wall wall, List<Sprite> sprites, ILocation[] locations)
    {
        _wall = wall;
        _sprites = sprites;
        _locations = locations;
    }
    private ILocation GetRandomLocation()
    {
        var index = _random.Next(_locations.Length);
        return _locations[index].Clone() as ILocation;
    }

    public void AddRandomLoc(Vector2 passPosition)
    {
        var locationGenerator = new LocationGenerator(GetRandomLocation(),_wall, _sprites, passPosition);
    }
}

public class DefoultLocation : ILocation
{
    public string Map { get; set; }
    public Rectangle Enter { get; set; }
    public Rectangle Exit { get; set; }
    public Rectangle Bound { get; set; }
    public bool IsCreateNext { get; set; }

    public DefoultLocation()
    {
        Parse();
    }
    
    public void Parse()
    {
        Map = File.ReadAllText("defoultLoc.txt");
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}

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

        var f = false;
        for (var i = 0; i < rows[0].Length; i++)
        for (var j = 0; j < rows.Length; j++)
        {
            var x =  (int)position.X + i * _spriteSize;
            var y =  (int)position.Y + j * _spriteSize;

            switch (rows[j][i])
            {
                case '#':
                    wall.AddWall(sprites, new Vector2(x, y), wall);
                    break;
                case 'e': 
                    _location.Exit = new Rectangle(new Point(x,y), new Point(x+_spriteSize, y+_spriteSize));
                    break;
                case 'E':
                    if (!f)
                    {
                        position = new Vector2(x - i * _spriteSize, y - 2 * j * _spriteSize);
                        i = 0;
                        j = 0;
                        f = true;
                    }

                    _location.Enter = new Rectangle(new Point(x,y), new Point(x+_spriteSize, y+_spriteSize));
                    break;
            }
        }

        CreatedLocation.Add(_location);
    }
}