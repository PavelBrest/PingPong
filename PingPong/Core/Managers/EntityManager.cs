using PingPong.Core.Entities.Abstractions;
using PingPong.Core.Managers.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PingPong.Core.Managers
{
	public class EntityManager : IEntityManager
    {
        private readonly Dictionary<string, IEntity> _entities = new Dictionary<string, IEntity>();

        public void AddEntity(IEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _entities.Add(entity.Id, entity);
        }

        public void Update()
        {
            foreach (var entity in _entities)
                entity.Value.Update();
        }

        public void CheckCollisions()
        {
            foreach ((_, var entity) in _entities)
            {
                var collision = _entities.FirstOrDefault(p => p.Key != entity.Id && p.Value.HasCollision(entity));
                if (collision.Key != default)
                    entity.OnCollision(collision.Value);
            }
        }

        public IEntity GetEntity(string id)
        {
	        return _entities[id];
        }

        public IReadOnlyList<IEntity> GetEntities()
        {
            return _entities.Values.ToList();
        }
    }
}
