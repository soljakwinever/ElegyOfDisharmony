using System;

namespace MessageboxSystem
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
#if !DEBUG
            using (frmCheckForUpdates updates = new frmCheckForUpdates())
            {
                if (updates.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
#endif
                    using (var game = new CutSceneGame())
                    {
                        game.Run();
                    }
#if !DEBUG
                }
            }
#endif
        }
    }
#endif
}

