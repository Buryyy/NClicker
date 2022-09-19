using Autofac;
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
            var builder = new ContainerBuilder();
            builder.RegisterModule<ContainerModule>();

            var container = builder.Build();
            Context = container.Resolve<IComponentContext>();
        }
    }
}
