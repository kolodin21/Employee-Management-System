using System.Windows;
using Client.GUI.View;
using Client.GUI.View.AdminPageMenu;
using Client.GUI.View.LogInSystem;
using Client.GUI.ViewModel;
using Client.GUI.ViewModel.AdminPageMenu;
using Client.GUI.ViewModel.LogInSystem;
using Client.HTTP;
using Microsoft.Extensions.DependencyInjection;

namespace Client.GUI
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; }
        public ManagerHttp ManagerHttp { get; }

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            //Получение View и ViewModel
            ConfigureServices(services);

            // Построение провайдера
            ServiceProvider = services.BuildServiceProvider();
            ManagerHttp = ServiceProvider.GetRequiredService<ManagerHttp>();

            ViewModelBase.Initialize(ServiceProvider,ManagerHttp);
        }


        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<EmployeeHttpClient>();
            services.AddSingleton<DepartmentHttpClient>();
            services.AddSingleton<PositionHttpClient>();
            services.AddSingleton<ReportHttpClient>();
            services.AddSingleton<LeaveHttpClient>();
            services.AddSingleton<ManagerHttp>();

            services.AddViewWithViewModel<MainMenuPageView,MainMenuPageViewModel>();
            services.AddViewWithViewModel<AuthorizationPageView, AuthorizationPageViewModel>();
            services.AddViewWithViewModel<AdminPageView, AdminPageViewModel>();


            services.AddViewWithViewModelTransient<EmployeeAllPageView, EmployeeAllPageViewModel>();
            services.AddViewWithViewModelTransient<AddEmployeePageView, AddEmployeePageViewModel>();
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
        public static IServiceCollection AddViewWithViewModelTransient<TView, TViewModel>(this IServiceCollection services)
            where TView : FrameworkElement, new()
            where TViewModel : class
        {
            services.AddTransient<TViewModel>();
            services.AddTransient<TView>(provider =>
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
