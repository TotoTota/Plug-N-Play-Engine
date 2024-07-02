using Silk.NET.OpenGL;

namespace PlugNPlay.Rendering
{
    public class VAO
    {
        private GL _gl = Graphics.GetOpenGL();
        private uint _vao;

        public VAO()
        {
            _vao = _gl.GenVertexArray();
            _gl.BindVertexArray(_vao);
        }

        public unsafe void LinkVBO(uint layout, int size, VBO vbo, GLEnum type, uint stride, void* offset)
        {
            vbo.Bind();
            _gl.VertexAttribPointer(layout, size, type, false, stride, offset);
            _gl.EnableVertexAttribArray(layout);
            vbo.Unbind();
        }

        public void Bind()
        {
            _gl.BindVertexArray(_vao);
        }

        public void Unbind()
        {
            _gl.BindVertexArray(0);
        }

        public void Delete()
        {
            _gl.DeleteVertexArray(_vao);
        }
    }
}