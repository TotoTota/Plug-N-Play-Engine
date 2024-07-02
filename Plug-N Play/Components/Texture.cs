using Silk.NET.OpenGL;
using StbImageSharp;

namespace PlugNPlay
{
    public class Texture
    {
        private uint _handle;
        private GL _gl;
        public bool HasAlpha { get; private set; }

        public unsafe Texture(GL gl, string path)
        {
            _gl = gl;

            _handle = _gl.GenTexture();
            Bind();

            using (Stream stream = File.OpenRead(path))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                HasAlpha = image.Comp == ColorComponents.RedGreenBlueAlpha;
                
                fixed (byte* ptr = image.Data)
                {
                    if (HasAlpha)
                    {
                        _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)image.Width, (uint)image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
                    }
                    else
                    {
                        _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgb, (uint)image.Width, (uint)image.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, ptr);
                    }
                }
            }

            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.Repeat);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.Repeat);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.Linear);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);

            _gl.GenerateMipmap(TextureTarget.Texture2D);
        }

        public void Bind(TextureUnit textureUnit = TextureUnit.Texture0)
        {
            _gl.ActiveTexture(textureUnit);
            _gl.BindTexture(TextureTarget.Texture2D, _handle);
        }

        public void Dispose()
        {
            _gl.DeleteTexture(_handle);
        }
    }
}