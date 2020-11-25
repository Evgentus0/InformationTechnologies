using DBMS.Manager.Exntentions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serviceProvider = SetupServices();
            Application.Run(new Main(serviceProvider));
        }

        private static IServiceProvider SetupServices()
        {
            var serviceProvider = new ServiceCollection();
            serviceProvider.AddManagers();

            return serviceProvider.BuildServiceProvider();
        }
    }
}
