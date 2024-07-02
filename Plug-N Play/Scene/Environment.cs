namespace PlugNPlay
{
    public class Environment
    {
        public List<Entity> Entities;

        public Environment()
        {
            Entities = new List<Entity>();
        }
        
        public T SpawnEntity<T>() where T : Entity, new()
        {
            T entity = new T();
            Entities.Add(entity);
            return entity;
        }

        public void KillEntity(Entity entity)
        {
            entity.OnKill();
            entity.IsKilled = true;
            Entities.Remove(entity);
        }
        
        public void RenderEntities(double deltaTime)
        {
            var sortedEntities = Entities.OrderBy(e => e.ZIndex).ToList();
            foreach (var entity in sortedEntities)
            {
                if (entity.IsKilled == false)
                {
                    entity.OnRender(deltaTime);
                }
                else
                {
                    
                }
            }
        }
    }
}