using Silk.NET.OpenGL;

namespace PlugNPlay.Rendering
{
    public class EBO
    {
        private GL _gl = Graphics.GetOpenGL();
        private uint _ebo;

        public unsafe EBO(uint[] indices)
        {
            _ebo = _gl.GenBuffer();
            _gl.BindBuffer(GLEnum.ElementArrayBuffer, _ebo);
            fixed (uint* buf = indices)
                _gl.BufferData(GLEnum.ElementArrayBuffer, (nuint)(indices.Length * sizeof(uint)), buf,
                    GLEnum.StaticDraw);
        }

        public void Bind()
        {
            _gl.BindBuffer(GLEnum.ElementArrayBuffer, _ebo);
        }

        public void Unbind()
        {
            _gl.BindBuffer(GLEnum.ElementArrayBuffer, 0);
        }

        public void Delete()
        {
            _gl.DeleteBuffer(_ebo);
        }
    }
}