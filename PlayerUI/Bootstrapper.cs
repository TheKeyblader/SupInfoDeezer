using PlayerUI.Views;
using System.Windows;
using Prism.Modularity;
using Autofac;
using Prism.Autofac;
using Player;
namespace PlayerUI
{
    class Bootstrapper : AutofacBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            //moduleCatalog.AddModule(typeof(YOUR_MODULE));
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);
            var config = new Connect_Config()
            {
                AppId = "263102",
                ProductId = "Test",
                ProductBuildId = "00001",
                UserProfilePath = "c:\\dzr\\dzrcache_NDK_SAMPLE"
            };
            builder.RegisterInstance<Connect>(new Connect(config)).SingleInstance();
            builder.RegisterType<Player.Player>().SingleInstance();
        }
    }
}
