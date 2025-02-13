using NLog;
using ReactiveUI.Fody.Helpers;
using System.Windows.Controls;
using Client.GUI.View;
using Client.GUI.ViewModel.LogInSystem;

namespace Client.GUI.ViewModel
{
    public static class NamePage
    {
        public static string MainMenu => "Главное меню";
        public static string Authorization => "Авторизация";
        public static string AddEmployee => "Добавление сотрудника";

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

            InitializeAndSubscribeToContentChanges<MainMenuPageViewModel>();
            InitializeAndSubscribeToContentChanges<AuthorizationPageViewModel>();

        }
        private void InitializeAndSubscribeToContentChanges<TViewModel>()
            where TViewModel : class
        {
            var viewModel = GetPage<TViewModel>();
            SubscribeToContentChanged(viewModel, (newContent, newTitle) =>
            {
                CurrentContent = newContent;
                Title = newTitle;
            });
        }

    }
}
