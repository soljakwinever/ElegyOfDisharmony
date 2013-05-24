using System;
using System.Threading;

namespace EquestriEngine.Data.Types
{
    public class ThreadedAction
    {
        public static void Invoke(Action action)
        {
            Thread thread = new Thread(new ThreadStart(action));
            thread.Start();
        }
    }
}
