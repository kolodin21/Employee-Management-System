using System.Reactive;
using System.Windows.Controls;
using Client.GUI.View.AdminPageMenu;
using NLog;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Client.GUI.ViewModel
{
    public class AdminPageViewModel : ViewModelBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        [Reactive] public UserControl? CurrentContentAdminPage { get; set; }

        //Основное меню администратора
        public ReactiveCommand<Unit, Unit> AllEmployeeCommand { get; }
        public ReactiveCommand<Unit, Unit> AddEmployeeCommand { get; }
        public ReactiveCommand<Unit, Unit> AccountingCommand { get; }
        public ReactiveCommand<Unit, Unit> GeneratingReportsCommand { get; }
        public ReactiveCommand<Unit, Unit> ChangingCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }

        [Reactive] public bool IsLoadEmployeeCommand { get; set; }
        [Reactive] public bool IsOpenAddEmployeeCommand { get; set; }
        [Reactive] public bool IsAccountingCommand { get; set; }
        [Reactive] public bool IsGeneratingReportsCommand { get; set; }
        [Reactive] public bool IsChangingCommand { get; set; }



        public AdminPageViewModel()
        {

            AllEmployeeCommand = ReactiveCommand.Create(() =>
            {
                CurrentContentAdminPage = GetPage<EmployeeAllPageView>();
                Reset();
                IsLoadEmployeeCommand = true;
            }, CanLoadEmployee());

            AddEmployeeCommand = ReactiveCommand.Create(() =>
            {
                CurrentContentAdminPage = GetPage<AddEmployeePageView>();
                Reset();
                IsOpenAddEmployeeCommand = true;
            });

            // Подписка на исключения, выбрасываемые командой
            AllEmployeeCommand.ThrownExceptions.Subscribe(ex =>
            {
               Logger.Warn($"Ошибка в AllEmployeeCommand: {ex}");
            });


            //AddEmployeeCommand = ReactiveCommand.Create(OpenAddEmployeePage);

            ExitCommand = ReactiveCommand.Create(ExecExit);
        }

        private IObservable<bool> CanLoadEmployee()
        {
            return this.WhenAnyValue(
                x => x.IsLoadEmployeeCommand, 
                (loadEmployee) => !loadEmployee);
        }

        private void Reset()
        {
            IsLoadEmployeeCommand = false;
            IsOpenAddEmployeeCommand = false;
            IsAccountingCommand = false;
            IsGeneratingReportsCommand = false;
            IsChangingCommand = false;
        }
    }
}
 