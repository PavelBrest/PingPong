using PingPong.Core.Commands.Abstractions;

namespace PingPong.Core.Managers.Abstractions
{
    public interface ICommandManager
    {
        void AddCommand(ICommand command);
        void ExecuteCommands();

        void ClearQueue();
    }
}