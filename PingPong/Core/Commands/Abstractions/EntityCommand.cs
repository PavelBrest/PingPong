using System;
using PingPong.Core.Entities.Abstractions;

namespace PingPong.Core.Commands.Abstractions
{
    public abstract class EntityCommand<TEntity> : ICommand
        where TEntity : class, IEntity
    {
        protected readonly TEntity _entity;

        protected EntityCommand(TEntity entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        public abstract void Execute();
    }
}