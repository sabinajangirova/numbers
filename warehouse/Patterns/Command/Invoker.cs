using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Patterns.Command
{
    public class Invoker
    {
        private ICommand Add;
        private Queue<ICommand> CommandsToDo;
        public Invoker()
        {
            CommandsToDo = new Queue<ICommand>();
        }

        public Invoker(Queue<ICommand> c)
        {
            CommandsToDo = c;
        }

        public void SetCommand(ICommand c)
        {
            CommandsToDo.Enqueue(c);
        }

        public void Run()
        {
            foreach(var c in CommandsToDo.Where(c => !c.IsCompleted))
            {
                c.Execute();
                c.IsCompleted = true;   
            }
            
        }

        public void Cancel()
        {
            foreach (var c in CommandsToDo.Where(c => c.IsCompleted))
            {
                c.Unexecute();
                c.IsCompleted = false;
            }
        }
    }
}
