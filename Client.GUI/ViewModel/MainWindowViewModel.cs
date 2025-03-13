using System.Windows;
using NLog;
using ReactiveUI.Fody.Helpers;
using System.Windows.Controls;
using Client.GUI.View;
using Client.GUI.ViewModel.LogInSystem;
using Client.GUI.View.LogInSystem;

namespace Client.GUI.ViewModel
{
    public static class NamePage
    {
        public static string MainMenu => "Главное меню";
        public static string Authorization => "Авторизация";
        public static string AddEmployee => "Добавление сотрудника";
        public static string Administrator => "Администратор";

    }

    public class MainWindowViewModel : ViewModelBase
    {
        //Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Reactive] public UserControl? CurrentContent { get; set; }
        [Reactive] public string Title { get; set; }

        public MainWindowViewModel()
        {
            Title = NamePage.MainMenu;
            CurrentContent = GetPage<MainMenuPageView>();

            InitializeAndSubscribeToContentChanges<MainMenuPageView,MainMenuPageViewModel>();
            InitializeAndSubscribeToContentChanges<AuthorizationPageView, AuthorizationPageViewModel>();
            InitializeAndSubscribeToContentChanges<AdminPageView, AdminPageViewModel>();

        }

        private void InitializeAndSubscribeToContentChanges<TView, TViewModel>()
            where TView : FrameworkElement
            where TViewModel : class
        {
            var view = GetPage<TView>(); // Получаем View
            if (view.DataContext is TViewModel viewModel) // Проверяем, что DataContext нужного типа
            {
                SubscribeToContentChanged(viewModel, (newContent, newTitle) =>
                {
                    CurrentContent = newContent;
                    Title = newTitle;
                });
            }
            else
            {
                throw new InvalidOperationException($"DataContext в {typeof(TView)} не является {typeof(TViewModel)}");
            }
        }
    }
}
