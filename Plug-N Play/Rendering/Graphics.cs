using System.Drawing;
using Silk.NET.OpenGL;

namespace PlugNPlay
{
    public class Graphics
    {
        private static GL _gl;
        
        public void SetBackgroundColor(Color color)
        {
            _gl.ClearColor(color);
        }

        public void Clear()
        {
            _gl.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void SetOpenGL(GL gl)
        {
            _gl = gl;
        }

        public static GL GetOpenGL()
        {
            return _gl;
        }
    }
}