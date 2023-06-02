using Microsoft.Xna.Framework;

namespace MyGame;

public class Camera
{
    public Matrix Transform { get; private set; }

    public void Follow(Sprite target)
    {
        var position = Matrix.CreateTranslation(
            -target.Position.X - (target.Bounds.Width / 2),
            -target.Position.Y - (target.Bounds.Height / 2),
            0);
        
        var centerOffset = Matrix.CreateTranslation(
            Game1.ScreenWight / 2,
            Game1.ScreenHeight / 2,
            0);

        Transform =  position * centerOffset;
    }
}