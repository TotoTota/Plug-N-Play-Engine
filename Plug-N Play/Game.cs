using System.Drawing;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using PlugNPlay.Rendering;

namespace PlugNPlay
{
    public class Game
    {
        private GameWindow _window;
        private Graphics _graphics;
        private GL _gl;
        private static VAO vao;
        private static EBO ebo;
        private static VBO vbo;
        private static ShaderGame shader;
        private static Camera _camera;
        private Icon _icon;
        private static Environment _environment;
        private static IInputContext input;
        private static Color _color = Color.White;
        
        private static readonly float[] vertices =
        [
            // Positions                    // Tex Coords
            -0.5f, -0.5f, 0.0f,             0.0f, 0.0f,
             0.5f, -0.5f, 0.0f,             1.0f, 0.0f,
             0.5f,  0.5f, 0.0f,             1.0f, 1.0f,
            -0.5f,  0.5f, 0.0f,             0.0f, 1.0f
        ];

        private static readonly uint[] indices =
        [
            0, 1, 2,
            2, 3, 0
        ];
        
        public void Start(GameWindow window, Graphics graphics, Camera camera, Environment environment)
        {
            _window = window;
            _graphics = graphics;
            _camera = camera;
            _environment = environment;

            window.GetWindow().Load += OnLoad;
            window.GetWindow().FramebufferResize += OnFramebufferResize;
            window.GetWindow().Update += OnUpdate;
            window.GetWindow().Render += OnRender;
            
            window.Run();
        }


        private unsafe void OnLoad()
        {
            input = _window.GetWindow().CreateInput();
            
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                foreach (var entity in _environment.Entities)
                {
                    input.Keyboards[i].KeyDown += entity.KeyDown;
                }
            }
            
            _gl = _window.GetWindow().CreateOpenGL();
            _graphics.SetOpenGL(_gl);
            
            shader = new ShaderGame("default.vert", "default.frag");

            vao = new VAO();

            vbo = new VBO(vertices);
            ebo = new EBO(indices);
            
            foreach (var entity in _environment.Entities)
            {
                if (entity.IsKilled == false)
                {
                    entity.OnStart();
                }
            }
            
            _icon = new Icon("..\\..\\..\\PlugNPlayLogo.png");

            var raw = _icon.Raw;
            _window.GetWindow().SetWindowIcon(new[] {raw});
        }

        private void OnUpdate(double deltaTime)
        {
            foreach (var entity in _environment.Entities)
            {
                if (entity.IsKilled == false)
                {
                    entity.OnUpdate(deltaTime);
                }
            }
        }
        
        private unsafe void OnRender(double deltaTime)
        {
            _graphics.Clear();
            
            vao.Bind();
            shader.Activate();
            
            var view = _camera.GetViewMatrix();
            var projection = _camera.GetProjectionMatrix(_window.GetWindow().Size.X / (float)_window.GetWindow().Size.Y);
            
            shader.UploadMatrix4x4("view", view);
            shader.UploadMatrix4x4("projection", projection);
            
            _environment.RenderEntities(deltaTime);

            _graphics.SetBackgroundColor(_color);
        }
        
        private void OnFramebufferResize(Vector2D<int> newSize)
        {
            _gl.Viewport(newSize);
        }

        public static VAO GetVAO()
        {
            return vao;
        }
        
        public static VBO GetVBO()
        {
            return vbo;
        }
        
        public static EBO GetEBO()
        {
            return ebo;
        }

        public static ShaderGame GetShader()
        {
            return shader;
        }
        
        public static uint[] GetIndices()
        {
            return indices;
        }

        public static IKeyboard GetInput()
        {
            return input.Keyboards[0];
        }
        
        public static Environment GetEnvironment()
        {
            return _environment;
        }

        public static void SetBackgroundColor(Color color)
        {
            _color = color;
        }
    }
}
