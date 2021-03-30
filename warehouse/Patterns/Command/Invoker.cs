using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace warehouse.Patterns.Command
{
    public class Invoker
    {
              
        private Queue<ICommand> CommandsToDo;
        public Invoker()
        {
            CommandsToDo = new Queue<ICommand>();
        }

        public void SetCommand(ICommand c)
        {
            CommandsToDo.Enqueue(c);
        }

        public void Run()
        {
            /*while(CommandsToDo.TryDequeue(out ICommand c))
            {
                c.Execute();
            }*/

            while(CommandsToDo.Count() != 0)
            {
                CommandsToDo.Dequeue().Execute();
            }
            
        }

    }
}
