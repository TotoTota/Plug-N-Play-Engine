#version 330 core

in vec2 TexCoords;

out vec4 FragColor;

uniform sampler2D ourTexture;
uniform bool useTexture;
uniform vec4 color;

void main()
{
    if (useTexture)
    {
        vec4 texColor = texture(ourTexture, TexCoords);
        FragColor = texColor * color;
    }
    else
    {
        FragColor = color;
    }
}