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
    
    private List<Sprite> _sprites;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _sprites = new List<Sprite>();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        var bullet = Content.Load<Texture2D>("bullet");
        _sprites.Add(new Player(Content.Load<Texture2D>("triangle"), new Vector2(100,100), 
            new Bullet(bullet)));
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var sprite in _sprites.ToArray())
        {
            sprite.Update(gameTime, _sprites);
        }

        for (int i = 0; i < _sprites.Count; i++)
        {
            if (_sprites[i].IsRemoved)
            {
                _sprites.RemoveAt(i);
                i--;
            }
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        
        foreach (var sprite in _sprites)
        {
            sprite.Draw(_spriteBatch);
        }
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}