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
    private Camera Camera { get; set; }
    public static Vector2 Cursor { get; private set; }
    private static Vector2 ScreenCenter => new (ScreenWight / 2, ScreenHeight / 2);
    private static int _score = 0;
    
    public static int ScreenHeight;
    public static int ScreenWight;
    
    public static List<Sprite> Sprites;
    private SpriteFont _font;
    private Texture2D _hpBarTexture;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Sprites = new List<Sprite>();
        ScreenHeight = _graphics.PreferredBackBufferHeight = 720;
        ScreenWight = _graphics.PreferredBackBufferWidth = 1440;
        _graphics.ApplyChanges();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        var bullet = new Bullet(Content.Load<Texture2D>("bullet"));
        _player = new Player(Content.Load<Texture2D>("triangle"), new Vector2(200, 200), bullet);

        _generatorEnemy = new GeneratorEnemy(new IPatternAttack[]
            {
                new DefaultAttack(Content.Load<Texture2D>("yellowEnemy")),
                new RoundAttack(Content.Load<Texture2D>("blueEnemy"))
            },
            Content.Load<Texture2D>("boss"),
            Sprites, _player, bullet);

        _hpBarTexture = Content.Load<Texture2D>("hpbar");

        CreateLabyrinthGenerator();

        _font = Content.Load<SpriteFont>("File");
        Camera = new Camera();
        
        Sprites.Add(_player);
    }

    private void CreateLabyrinthGenerator()
    {
        var wall = new Wall(Content.Load<Texture2D>("wall"));
        
        var location = new ILocation[]
        {
            new DefoultLocation(),
            new SecLocation()
        };
        _labyrinthGenerator = new LabyrinthGenerator(wall, Sprites, location);
        _labyrinthGenerator.AddStartLoc(new Vector2());
    }

    private void SearchCursor()
    {
        Cursor = Mouse.GetState().Position.ToVector2();
        Cursor = new Vector2(-(ScreenCenter.X - _player.Bounds.Width/2 - Cursor.X) + _player.Position.X,
            -(ScreenCenter.Y - _player.Bounds.Height/2 - Cursor.Y) + _player.Position.Y);
        Camera.Follow(_player);
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
                if (Sprites[i] is Enemy)
                {
                    if (Sprites[i] is Boss)
                    {
                        _bossIsdead = true;
                        _score += 100;
                    }

                    _score += 10;
                }

                Sprites.RemoveAt(i);
                i--;
            }
    }

    private bool _bossIsdead;
    private void WinGame()
    {
        if (bossIsSpawn && _bossIsdead)
        {
            var pos = new Vector2(_player.Position.X - 50, _player.Position.Y - 100);
            Sprites.Clear();
            _spriteBatch.DrawString(_font, $"you win\nscore: {_score}", pos, Color.Red);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        SearchCursor();
        CreateLab();
        SpawnBoss();
        
        _generatorEnemy.AddRandomEnemy(gameTime);

        UpdateSprite(gameTime);
        
        base.Update(gameTime);
    }

    private bool bossIsSpawn;
    private void SpawnBoss()
    {
        if (LocationGenerator.CreatedLocation.Count >= 5 && !bossIsSpawn)
        {
            _generatorEnemy.SpawnBoss();
            bossIsSpawn = true;
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: Camera.Transform);

        CheckOnPlayerDead();
        WinGame();
        // {
        //     var pos = new Vector2(_player.Position.X + ScreenWight / 2 - 20,
        //         _player.Position.Y - ScreenHeight / 2 + 50);
        //     _spriteBatch.DrawString(_font, $"{_score}", pos, Color.Red);
        // }

        DrawSprites();

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawSprites()
    {
        foreach (var sprite in Sprites)
        {
            sprite.Draw(_spriteBatch);
            if(sprite is Movable movable)
                _spriteBatch.Draw(_hpBarTexture, new Rectangle((int)movable.Position.X - 24, (int)movable.Position.Y + 50,
                    (int)movable.Hp / 2, 20), Color.Red);
        }
    }
    
    private bool CheckOnPlayerDead()
    {
        if (_player.IsRemoved)
        {
            var pos = new Vector2(_player.Position.X -50, _player.Position.Y-100);
            Sprites.Clear();
            _spriteBatch.DrawString(_font, $"you lose\n score: {_score}", pos, Color.Red);
            return true;
            //restartGame
        }

        return false;
    }
}