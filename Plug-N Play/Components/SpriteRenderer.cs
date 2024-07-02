using System.Numerics;
using Silk.NET.OpenGL;
using PlugNPlay.Rendering;

namespace PlugNPlay;

public class SpriteRenderer
{
    private Vector3 _Position = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 _Size = new Vector3(1.0f, 1.0f, 1.0f);
    private float _rotationZ = 0.0f;
    private Texture _texture;
    private Vector4 _color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
    private GL _gl;
    private VAO vao;
    private EBO ebo;
    private VBO vbo;
    private ShaderGame shader;
    private Matrix4x4 transform = Matrix4x4.Identity;
    
    public unsafe SpriteRenderer(Vector3 pos, Vector3 size, float rotation)
    {
        // _killRenderer = false;
        _gl = Graphics.GetOpenGL();
        _Position = pos;
        _Size = size;
        _rotationZ = rotation;
        
        shader = Game.GetShader();
        
        vao = Game.GetVAO();

        vbo = Game.GetVBO();
        ebo = Game.GetEBO();
            
        vao.LinkVBO(0, 3, vbo, GLEnum.Float, 5 * sizeof(float), (void*)0);
        vao.LinkVBO(1, 2, vbo, GLEnum.Float, 5 * sizeof(float), (void*)(3 * sizeof(float)));
            
        vao.Unbind();
    }
    
    public void SetTexture(string path)
    {
        _texture = new Texture(_gl, path);
    }

    public void SetColor(Vector4 newColor)
    {
        _color = newColor;
    }

    public void Update()
    {
        transform = Matrix4x4.CreateScale(_Size)
                    * Matrix4x4.CreateRotationZ(_rotationZ)
                    * Matrix4x4.CreateTranslation(_Position);
    }

    public unsafe void Render()
    {
        shader.UploadMatrix4x4("transform", transform);
        shader.UploadVector4("color", _color);

        if (_texture != null)
        {
            _texture.Bind();
            shader.UploadInt("useTexture", 1);

            if (_texture.HasAlpha)
            {
                _gl.Enable(EnableCap.Blend);
                _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            }
            else
            {
                _gl.Disable(EnableCap.Blend);
            }
        }
        else
        {
            shader.UploadInt("useTexture", 0);
            _gl.Enable(EnableCap.Blend);
            _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        _gl.DrawElements(PrimitiveType.Triangles, (uint)Game.GetIndices().Length, DrawElementsType.UnsignedInt,
            (void*)0);
    }

    public void SetPosition(Vector3 pos)
    {
        _Position = pos;
    }
    
    public void SetPositionX(float posX)
    {
        _Position.X = posX;
    }
    
    public void SetPositionY(float posY)
    {
        _Position.Y = posY;
    }
    
    public void SetPositionZ(float posZ)
    {
        _Position.Z = posZ;
    }
    
    public void SetSize(Vector3 size)
    {
        _Size = size;
    }
    
    public void SetSizeX(float sizeX)
    {
        _Size.X = sizeX;
    }
    
    public void SetSizeY(float sizeY)
    {
        _Size.Y = sizeY;
    }
    
    public void SetSizeZ(float sizeZ)
    {
        _Size.Z = sizeZ;
    }

    public void SetRotationZ(float newRotation)
    {
        _rotationZ = newRotation;
    }
}