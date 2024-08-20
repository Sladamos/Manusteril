﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    internal class ExitProgramCommand : ICommand
    {
        public string Name => "Wyjscie";

        public string Description => "Wyjście z programu";

        public event Action? ProgramExited;

        public void Execute()
        {
            ProgramExited?.Invoke();
        }
    }
}
