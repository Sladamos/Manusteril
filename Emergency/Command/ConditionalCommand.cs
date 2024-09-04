using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    internal class ConditionalCommand : ICommand
    {
        public string Name => command.Name;

        public string Description => command.Description;

        private Condition condition;

        private ICommand command;

        public ConditionalCommand(ICommand command, Condition condition)
        {
            this.condition = condition;
            this.command = command;
        }

        public async Task Execute()
        {
            if (condition.Predicate())
            {
                await command.Execute();
            } else
            {
                Console.WriteLine($"Nie można teraz wykonać tej komendy: {condition.Reason}");
            }
        }
    }
}
