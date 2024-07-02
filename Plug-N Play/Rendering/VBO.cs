using Silk.NET.OpenGL;

namespace PlugNPlay.Rendering
{
    public class VBO
    {
        private GL _gl = Graphics.GetOpenGL();
        private uint vbo;

        public unsafe VBO(float[] vertices)
        {
            vbo = _gl.GenBuffer();
            _gl.BindBuffer(GLEnum.ArrayBuffer, vbo);
            fixed (float* buf = vertices)
                _gl.BufferData(GLEnum.ArrayBuffer, (nuint)(vertices.Length * sizeof(float)), buf, GLEnum.StaticDraw);
        }

        public void Bind()
        {
            _gl.BindBuffer(GLEnum.ArrayBuffer, vbo);
        }

        public void Unbind()
        {
            _gl.BindBuffer(GLEnum.ArrayBuffer, 0);
        }

        public void Delete()
        {
            _gl.DeleteBuffer(vbo);
        }
    }
}