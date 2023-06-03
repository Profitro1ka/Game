﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _player;
    private GeneratorEnemy _generatorEnemy;
    private LabyrinthGenerator _labyrinthGenerator;
    private Camera _camera { get; set; }
    public static Vector2 Cursor { get; private set; }
    private static Vector2 ScreenCenter => new (ScreenWight / 2, ScreenHeight / 2);
    
    public static int ScreenHeight;
    public static int ScreenWight;
    
    public static List<Sprite> Sprites;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Sprites = new List<Sprite>();
        ScreenHeight = _graphics.PreferredBackBufferHeight;
        ScreenWight = _graphics.PreferredBackBufferWidth;
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        var bullet = new Bullet(Content.Load<Texture2D>("bullet"));
        _player = new Player(Content.Load<Texture2D>("triangle"), new Vector2(100, 100), bullet);

        _generatorEnemy = new GeneratorEnemy(new IPatternAttack[]
            {
                new DefaultAttack(Content.Load<Texture2D>("yellowEnemy")),
                new RoundAttack(Content.Load<Texture2D>("blueEnemy"))
            },
            Sprites, _player, bullet);

        CreateLabyrinthGenerator();
        _camera = new Camera();
        
        Sprites.Add(_player);
    }

    private void CreateLabyrinthGenerator()
    {
        var wall = new Wall(Content.Load<Texture2D>("wall"));
        var location = new ILocation[] { new DefoultLocation() };
        _labyrinthGenerator = new LabyrinthGenerator(wall, Sprites, location);
        _labyrinthGenerator.AddRandomLoc(new Vector2());
    }

    private void SearchCursor()
    {
        Cursor = Mouse.GetState().Position.ToVector2();
        Cursor = new Vector2(-(ScreenCenter.X - Cursor.X) + _player.Position.X, -(ScreenCenter.Y - Cursor.Y) + _player.Position.Y);
        _camera.Follow(_player);
    }

    private void CreateLab()
    {
        foreach (var location in LocationGenerator.CreatedLocation.ToArray())
            if (_player.Bounds.Intersects(location.Exit) && !location.IsCreateNext)
            {
                _labyrinthGenerator.AddRandomLoc(location.Exit.Location.ToVector2());
                location.IsCreateNext = true;
            }
    }

    private void UpdateSprite(GameTime gameTime)
    {
        foreach (var sprite in Sprites.ToArray())
            sprite.Update(gameTime, Sprites);

        for (var i = 0; i < Sprites.Count; i++)
            if (Sprites[i].IsRemoved)
            {
                Sprites.RemoveAt(i);
                i--;
            }
    }

    protected override void Update(GameTime gameTime)
    {
        SearchCursor();
        CreateLab();
        
        _generatorEnemy.AddRandomEnemy(gameTime);

        UpdateSprite(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _camera.Transform);
        
        foreach (var sprite in Sprites)
            sprite.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}