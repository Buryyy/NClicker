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
            //Storages
            builder.Register(c => new PresetStorage()).As<IPresetStorage>().SingleInstance();

            //Services
            builder.Register(c => new PresetService(c.Resolve<IPresetStorage>()))
                .As<IPresetService>().SingleInstance();
            builder.Register(c => new MouseControllerService(new System.Random()))
                .As<IMouseControllerService>().SingleInstance();

            builder.Register(c => new GlobalKeyboardHook()).SingleInstance();

            builder.Register(c => new KeyboardService(
                c.Resolve<GlobalKeyboardHook>())).As<IKeyboardService>().SingleInstance();

            builder.Register(c => new MainViewModel(
                c.Resolve<IPresetStorage>(), c.Resolve<IPresetService>(),
                c.Resolve<IKeyboardService>())).SingleInstance();
        }
    }
}