using Autofac;
using System;
using System.IO;
using System.Windows;

namespace NClicker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IComponentContext Context { get; set; }

        public App()
        {

            CreateAppDirectory();
            var builder = new ContainerBuilder();
            builder.RegisterModule<ContainerModule>();

            var container = builder.Build();
            Context = container.Resolve<IComponentContext>();
        }

        private void CreateAppDirectory()
        {
            Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\NClicker");
        }
    }
}
