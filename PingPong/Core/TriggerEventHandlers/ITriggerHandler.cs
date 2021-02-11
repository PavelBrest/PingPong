using PingPong.Core.Entities.Abstractions;
using PingPong.Core.Entities.Primitives;

namespace PingPong.Core.TriggerEventHandlers
{
	public interface ITriggerHandler
	{
		void Handle(Trigger trigger, IEntity entity);
	}
}