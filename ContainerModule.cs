using Autofac;
using NClicker.Hooks;
using NClicker.Services;
using NClicker.Storage;
using NClicker.ViewModels;

namespace NClicker
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IStorage>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //Storages
            builder.Register(c => new PresetStorage()).SingleInstance();

            //Services
            builder.Register(c => new PresetService()).SingleInstance();

            builder.Register(c => new MouseControllerService()).SingleInstance();
            builder.Register(c => new GlobalKeyboardHook()).SingleInstance();

            builder.Register(c => new KeyboardControllerService(
                c.Resolve<GlobalKeyboardHook>())).SingleInstance();

            builder.Register(c => new MainViewModel(
                c.Resolve<PresetStorage>(), c.Resolve<PresetService>(),
                c.Resolve<KeyboardControllerService>())).SingleInstance();
        }
    }
}