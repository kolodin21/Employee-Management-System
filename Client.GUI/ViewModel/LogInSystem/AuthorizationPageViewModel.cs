using NLog;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using System.Reactive;
using System.Windows;
using Client.GUI.Configuration;
using Client.GUI.View;

namespace Client.GUI.ViewModel.LogInSystem
{
    public class AuthorizationPageViewModel : ViewModelBase
    {
        //Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Reactive] public string? Login { get; set; } = "Admin";
        [Reactive] public string? Password { get; set; } = "Admin";
        public ReactiveCommand<Unit, Unit> EnterCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public AuthorizationPageViewModel()
        {
            EnterCommand = ReactiveCommand.Create(ExecEnter, CanExecEnter());
            BackCommand = ReactiveCommand.Create(ExecBack);
        }

        private void ExecEnter()
        {
            if (AdminConfig.Login == Login && AdminConfig.Password == Password)
            {
                Logger.Info($"Администратор:{Login} зашёл в систему");
                RaiseContentChanged(GetPage<AdminPageView>(), NamePage.Administrator);
            }
            else
            {
                ClearFields();
                Logger.Warn($"Попытка входа в систему с логином:{Login} и паролем:{Password}");
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecBack()
        {
            ClearFields();
            RaiseContentChanged(GetPage<MainMenuPageView>(), NamePage.MainMenu);
        }

        private IObservable<bool> CanExecEnter()
        {
            return this.WhenAnyValue(
                vm => vm.Login,
                vm => vm.Password,
                (login, password) =>
                    !string.IsNullOrEmpty(login) &&
                    !string.IsNullOrEmpty(password)
            );
        }

        private void ClearFields()
        {
            Login = string.Empty;
            Password = string.Empty;
        }
    }
}
