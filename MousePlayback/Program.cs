using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MousePlayback
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string resource1 = "MousePlayback.Bunifu_UI_v1.5.3.dll";
            EmbeddedAssembly.Load(resource1, "Bunifu_UI_v1.5.3.dll");
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            using (Mutex mutex = new Mutex(false, "Global\\" + "MousePlayback"))
            {
                if (!mutex.WaitOne(0, false))
                {
                    //one instance of remindme already running                                                                                                
                    if (args.Length > 0) {   }

                    return;
                }

                // Set the unhandled exception mode to force all Windows Forms errors to go through our handler.                
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

                // Add the event handler for handling non-UI thread exceptions to the event. 
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


                Application.Run(new Form1());
            }
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e.Exception != null)
            {
                ShowError(e.Exception);
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {                        
            Exception ex = (Exception)e.ExceptionObject;
            if (ex != null)
            {                
                ShowError(ex);                
            }
        }

        private static void ShowError(Exception ex)
        {
            ErrorPopup pop = new ErrorPopup(ex);
            pop.Show();
        }
    }
}
