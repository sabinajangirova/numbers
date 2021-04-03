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
              
        private ConcurrentQueue<ICommand> CommandsToDo;
        public Invoker()
        {
            CommandsToDo = new ConcurrentQueue<ICommand>();
        }

        public void SetCommand(ICommand c)
        {
            CommandsToDo.Enqueue(c);
            Run();
        }

        public void Run()
        {
            //while (CommandsToDo.IsEmpty)
            //{
            //    Thread.Sleep(3000);
            //}
            while(CommandsToDo.TryDequeue(out ICommand c))
            {                
                c.Execute();
            }
        }

    }
}
