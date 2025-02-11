using NLog;
using ReactiveUI.Fody.Helpers;
using System.Windows.Controls;

namespace Client.GUI.ViewModel
{
    public static class NamePage
    {
        public static string MainMenu => "Главное меню";
        public static string Authorization => "Авторизация";

    }


    public class MainWindowViewModel
    {
        //Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Reactive] public UserControl? CurrentContent { get; set; }
        [Reactive] public string Title { get; set; }

        public MainWindowViewModel()
        {
            Title = NamePage.MainMenu;
        }


    }
}
