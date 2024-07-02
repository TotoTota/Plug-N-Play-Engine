using System.Drawing;
using System.Numerics;

namespace PlugNPlay
{
    public class Example
    {
        public static void Main(string[] args)
        {
            var window = new GameWindow("Test Engine", 1280, 720);
            var graphics = new Graphics();
            var camera = new Camera(new Vector3(0.0f, 0.0f, 3.0f), Vector3.UnitY, -90.0f, 0.0f);
            var environment = new Environment();
            camera.Zoom = 90.0f;
            Game.SetBackgroundColor(Color.CornflowerBlue);
            new Game().Start(window, graphics, camera, environment);
        }
    }
}