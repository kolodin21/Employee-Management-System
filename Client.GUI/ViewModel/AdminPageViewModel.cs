using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Controls;
using Client.GUI.View;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Client.GUI.ViewModel
{
    public class AdminPageViewModel : ViewModelBase
    {
        [Reactive] public UserControl? CurrentContentAdminPage { get; set; }

        //Основное меню администратора
        public ReactiveCommand<Unit, Unit> LoadEmployeeCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenAddEmployeeCommand { get; }
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

            LoadEmployeeCommand = ReactiveCommand.Create(() =>
            {
                CurrentContentAdminPage = GetPage<EmployeeAllPageView>();
                Reset();
                IsLoadEmployeeCommand = true;
            }, CanLoadEmployee());

            //LoadEmployeeCommand.ThrownExceptions.Subscribe(ex =>
            //{
            //    Debug.WriteLine($"Ошибка в LoadEmployeeCommand: {ex}");
            //});

            //// Выполняем команду безопасно
            //Observable.Start(() => LoadEmployeeCommand.Execute().Subscribe());

            //LoadEmployeeCommand.Subscribe(_ =>
            //{
            //    IsLoadEmployeeCommand = false; // Сброс состояния
            //});


            //OpenAddEmployeeCommand = ReactiveCommand.Create(OpenAddEmployeePage);
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
 