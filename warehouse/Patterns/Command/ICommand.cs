using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Patterns.Command
{
    public interface ICommand
    {
        bool IsCompleted { get; set; }
        void Execute();
        void Unexecute();
    }
}
