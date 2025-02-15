using System.Reactive;
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


        public AdminPageViewModel()
        {
            LoadEmployeeCommand = ReactiveCommand.Create(() =>
            {
               CurrentContentAdminPage = GetPage<EmployeeAllPageView>();
            });
            

            OpenAddEmployeeCommand = ReactiveCommand.Create(OpenAddEmployeePage);
        }

        private void OpenAddEmployeePage()
        {

        }
    }
}
