using System.Numerics;
using Silk.NET.OpenGL;

namespace PlugNPlay.Rendering
{
    public class ShaderGame
    {
        private GL _gl = Graphics.GetOpenGL();  
        private uint Handle;
        
        public ShaderGame(string vertexPath, string fragmentPath)
        {
            string vertexSource = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\" + vertexPath));
            string fragmentSource = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\" + fragmentPath));

            uint vertexShader = _gl.CreateShader(GLEnum.VertexShader);
            _gl.ShaderSource(vertexShader, vertexSource);
            _gl.CompileShader(vertexShader);
            CheckShaderCompileErrors(vertexShader);
            
            uint fragmentShader = _gl.CreateShader(GLEnum.FragmentShader);
            _gl.ShaderSource(fragmentShader, fragmentSource);
            _gl.CompileShader(fragmentShader);
            CheckShaderCompileErrors(fragmentShader);

            Handle = _gl.CreateProgram();
            _gl.AttachShader(Handle, vertexShader);
            _gl.AttachShader(Handle, fragmentShader);
            _gl.LinkProgram(Handle);
            
            _gl.DetachShader(Handle, vertexShader);
            _gl.DetachShader(Handle, fragmentShader);
            _gl.DeleteShader(vertexShader);
            _gl.DeleteShader(fragmentShader);
        }

        public void Activate() => _gl.UseProgram(Handle);

        public void Delete()
        {
            _gl.DeleteProgram(Handle);
        }

        public unsafe void UploadMatrix4x4(string name, Matrix4x4 value)
        {
            int location = _gl.GetUniformLocation(Handle, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            _gl.UniformMatrix4(location, 1, false, (float*)&value);
        }
        
        public unsafe void UploadInt(string name, int value)
        {
            int location = _gl.GetUniformLocation(Handle, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            _gl.Uniform1(location, value);
        }
        
        public unsafe void UploadVector4(string name, Vector4 value)
        {
            int location = _gl.GetUniformLocation(Handle, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            _gl.Uniform4(location, value);
        }
        
        private void CheckShaderCompileErrors(uint shader)
        {
            _gl.GetShader(shader, GLEnum.CompileStatus, out int status);
            if (status == (int)GLEnum.False)
            {
                _gl.GetShaderInfoLog(shader, out string infoLog);
                Console.WriteLine($"Shader compilation error: {infoLog}");
            }
        }
    }
}