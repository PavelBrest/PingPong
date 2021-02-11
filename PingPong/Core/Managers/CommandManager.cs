using System;
using System.Collections.Generic;
using PingPong.Core.Commands.Abstractions;
using PingPong.Core.Managers.Abstractions;

namespace PingPong.Core.Managers
{
    public class CommandManager : ICommandManager
    {
	    private readonly Queue<ICommand> _holdingCommands = new Queue<ICommand>();
        private readonly Queue<ICommand> _commands = new Queue<ICommand>();

        private bool _isReading;

        public void AddCommand(ICommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            if (_isReading)
	            _holdingCommands.Enqueue(command);
            else
				_commands.Enqueue(command);
        }

        public void ExecuteCommands()
        {
	        _isReading = true;

            while (_commands.TryDequeue(out var command)) 
	            command.Execute();

            _isReading = false;

            while (_holdingCommands.TryDequeue(out var command))
	            _commands.Enqueue(command);
        }

        public void ClearQueue()
        {
	        _commands.Clear();
        }
    }
}
