﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Command
{
    internal interface ICommand
    {
        string Name { get; }
        string Description { get; }
        Task Execute();
    }
}
