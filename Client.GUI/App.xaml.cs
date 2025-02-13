using System.Windows;
using Client.GUI.View;
using Client.GUI.View.LogInSystem;
using Client.GUI.ViewModel;
using Client.GUI.ViewModel.LogInSystem;
using Microsoft.Extensions.DependencyInjection;

namespace Client.GUI
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; }

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            //Получение View и ViewModel
            ConfigureServices(services);

            // Построение провайдера
            ServiceProvider = services.BuildServiceProvider();

            ViewModelBase.Initialize(ServiceProvider);
        }


        private static void ConfigureServices(IServiceCollection services)
        {

            services.AddViewWithViewModel<MainMenuPageView,MainMenuPageViewModel>();
            services.AddViewWithViewModel<AuthorizationPageView, AuthorizationPageViewModel>();

        }

    }


    //Метод расширения для IServiceCollection для одновременного создадания View и ViewModel с привязкой DataContext
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddViewWithViewModel<TView, TViewModel>(this IServiceCollection services)
            where TView : FrameworkElement, new()
            where TViewModel : class
        {
            services.AddSingleton<TViewModel>();
            services.AddSingleton<TView>(provider =>
            {
                var view = new TView
                {
                    DataContext = provider.GetRequiredService<TViewModel>()
                };
                return view;
            });
            return services;
        }
    }
}
