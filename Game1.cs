using System.Collections.Generic;
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
    private Camera _camera { get; set; }
    public static Vector2 Cursor { get; private set; }
    public static Vector2 ScreenCenter => new (ScreenWight / 2, ScreenHeight / 2);
    
    public static int ScreenHeight;
    public static int ScreenWight;
    
    
    public static List<Sprite> Sprites;// невероятно тупой костыль 

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

        _player = new Player(Content.Load<Texture2D>("triangle"), new Vector2(0, 0), bullet);
        
        _generatorEnemy = new GeneratorEnemy(new IPatternAttack[] { 
            new DefaultAttack(Content.Load<Texture2D>("квадрат")), 
            new RoundAttack(Content.Load<Texture2D>("bluesquare"))}, 
            Sprites, _player, bullet);
        
        _camera = new Camera();
        
        Sprites.Add(_player);
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        Cursor = Mouse.GetState().Position.ToVector2();          
        
        Cursor = new Vector2(-(ScreenCenter.X - Cursor.X) + _player.Position.X, -(ScreenCenter.Y - Cursor.Y) + _player.Position.Y);
        _camera.Follow(_player);

        _generatorEnemy.AddRandomEnemy(gameTime);

        foreach (var sprite in Sprites.ToArray())
        {
            sprite.Update(gameTime, Sprites);
        }

        for (var i = 0; i < Sprites.Count; i++)
        {
            if (Sprites[i].IsRemoved)
            {
                Sprites.RemoveAt(i);
                i--;
            }
        }
        
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