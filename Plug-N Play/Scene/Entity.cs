using System.Numerics;
using Silk.NET.Input;

namespace PlugNPlay
{
    public abstract class Entity
    {
        private Dictionary<Type, object> _components = new Dictionary<Type, object>();

        public Vector3 Position = new Vector3(0.0f, 0.0f, 0.0f);
        public Vector3 Size = new Vector3(1.0f, 1.0f, 1.0f);
        public float RotationZ = 0.0f;
        public float ZIndex = 0f;
        public bool IsKilled;
        private bool once;
        
        public void AddComponent<T>(T component) where T : class
        {
            Type type = typeof(T);
            if (!_components.ContainsKey(type))
            {
                _components[type] = component;
            }
            else
            {
                throw new Exception($"Already has {component} component");
            }
        }
        
        public T? GetComponent<T>() where T : class
        {
            Type type = typeof(T);
            if (_components.ContainsKey(type))
            {
                return _components[type] as T;
            }
            return null;
        }
        
        public bool HasComponent<T>() where T : class
        {
            Type type = typeof(T);
            return _components.ContainsKey(type);
        }
        
        public void RemoveComponent<T>()
        {
            Type type = typeof(T);
            if (_components.ContainsKey(type))
            {
                _components.Remove(type);
            }
        }

        private IKeyboard GetInput()
        {
            return Game.GetInput();
        }

        protected bool IsKeyPressed(Key key)
        {
            return GetInput().IsKeyPressed(key);
        }

        private Environment GetEnvironment()
        {
            return Game.GetEnvironment();
        }

        public void KillEntity(Entity entity)
        {
            if (!once)
            {
                GetEnvironment().KillEntity(entity);
                once = true;
            }
        }

        public abstract void KeyDown(IKeyboard keyboard, Key key, int keyCode);
        public abstract void OnUpdate(double deltaTime);
        public abstract void OnRender(double deltaTime);
        public abstract void OnStart();
        public abstract void OnKill();
    }
}