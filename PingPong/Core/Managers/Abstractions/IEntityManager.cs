using System.Collections.Generic;
using PingPong.Core.Entities.Abstractions;

namespace PingPong.Core.Managers.Abstractions
{
    public interface IEntityManager
    {
        void AddEntity(IEntity entity);
        IEntity GetEntity(string id);
        IReadOnlyList<IEntity> GetEntities();
        void Update();
        void CheckCollisions();
    }
}