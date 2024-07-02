using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace PlugNPlay
{
    public class GameWindow
    {
        private static IWindow _window;

        public GameWindow(string title, int width, int height, bool maximized = false)
        {
            if (width <= 0 || height <= 0)
            {
                throw new Exception("Width or height is invalid");
            }

            if (title.Length > 55)
            {
                throw new Exception("Title is too long");
            }

            WindowOptions options = WindowOptions.Default with
            {
                Size = new Vector2D<int>(width, height),
                Title = title + " // Plug-N Play",
            };
            
            _window = Window.Create(options);

            if (maximized)
                _window.WindowState = WindowState.Maximized;
        }

        public void Run()
        {
            _window.Run();
        }

        public IWindow GetWindow()
        {
            return _window;
        }
    }
}