using System.Numerics;

namespace PlugNPlay
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Front { get; set; }
        public Vector3 Up { get; set; }
        public Vector3 Right { get; set; }
        public Vector3 WorldUp { get; set; }
        
        public float Yaw { get; set; }
        public float Pitch { get; set; }
        
        public float Zoom { get; set; }
        
        public Camera(Vector3 position, Vector3 up, float yaw, float pitch)
        {
            Position = position;
            WorldUp = up;
            Yaw = yaw;
            Pitch = pitch;
            Front = new Vector3(0.0f, 0.0f, -1.0f);
            Zoom = 45.0f;
            UpdateCameraVectors();
        }
        
        public Matrix4x4 GetViewMatrix()
        {
            return Matrix4x4.CreateLookAt(Position, Position + Front, Up);
        }
        
        public Matrix4x4 GetProjectionMatrix(float aspectRatio)
        {
            return Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 180f * Zoom, aspectRatio, 0.1f, 100.0f);
        }

        private void UpdateCameraVectors()
        {
            Vector3 front;
            front.X = MathF.Cos(MathF.PI / 180f * Yaw) * MathF.Cos(MathF.PI / 180f * Pitch);
            front.Y = MathF.Sin(MathF.PI / 180f * Pitch);
            front.Z = MathF.Sin(MathF.PI / 180f * Yaw) * MathF.Cos(MathF.PI / 180f * Pitch);
            Front = Vector3.Normalize(front);
            Right = Vector3.Normalize(Vector3.Cross(Front, WorldUp));
            Up = Vector3.Normalize(Vector3.Cross(Right, Front));
        }
    }
}