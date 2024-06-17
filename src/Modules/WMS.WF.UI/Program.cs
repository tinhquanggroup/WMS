using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WMS.WF.Application;
using WMS.WF.Infrastructure;

namespace WMS.WF.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = ConfigureServices;
            var serviceProvider = services.BuildServiceProvider();

            System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.SystemAware);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(serviceProvider.GetRequiredService<Form1>());
        }

        private static IServiceCollection ConfigureServices
        {
            get
            {
                var services = new ServiceCollection();
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                services.AddInfrastructure(configuration);
                services.AddApplication();

                services.AddTransient<Form1>();

                return services;
            }
        }
    }
}