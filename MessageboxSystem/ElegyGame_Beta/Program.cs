#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace ElegyGame_Beta
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (var game = new CutSceneGame())
            {
                game.Run();
            }
        }
    }
#endif
}
