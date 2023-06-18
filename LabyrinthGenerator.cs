using System;
using System.Collections.Generic;
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
    
    public void AddStartLoc(Vector2 passPosition)
    {
        var locationGenerator = new LocationGenerator(new StartLocation(), _wall, _sprites, passPosition);
    }
}