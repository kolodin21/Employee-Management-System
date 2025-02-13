using System.Reactive;
using Client.GUI.View.LogInSystem;
using ReactiveUI;

namespace Client.GUI.ViewModel.LogInSystem
{
    public class MainMenuPageViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> LoginCommand { get; }
        public ReactiveCommand<Unit, Unit> RegistrationCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }


        //TODO: Добавить команду для перехода на страницу регистрации

        public MainMenuPageViewModel()
        {
            LoginCommand = ReactiveCommand.Create(() =>
                RaiseContentChanged(GetPage<AuthorizationPageView>(), NamePage.Authorization));

            ExitCommand = ReactiveCommand.Create(ExecExit);
        }

    }
}
