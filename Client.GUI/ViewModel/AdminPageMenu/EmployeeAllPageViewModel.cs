using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using Client.GUI.Components;
using Models;
using NLog;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Client.GUI.ViewModel.AdminPageMenu
{
    public class EmployeeAllPageViewModel : ViewModelBase, IEmployeeForm
    {

        //Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private EmployeeDto? OriginalEmployee { get; set; }

        [Reactive] public EmployeeDto? SelectedEmployee { get; set; }

        [Reactive] public bool IsOpenWindowEdit { get; set; } 
        [Reactive] public bool IsOpenWindowAllEmployee { get; set; } = true;
        [Reactive] public bool IsActiveButtonEdit { get; set; } = true;

        #region IEmployeeForm

        [Reactive] public string? Name { get; set; }
        [Reactive] public string? Surname { get; set; }
        [Reactive] public string? Patronymic { get; set; }
        [Reactive] public DateTime HireDate { get; set; }
        [Reactive] private DateTime DateMissed { get; set; }
        [Reactive] public bool IsDismiss { get; set; } = false;

        [Reactive] public List<KeyValuePair<int, string>>? DepartmentsList { get; set; }
        [Reactive] public List<KeyValuePair<int, string>>? PositionList { get; set; }

        //Выбранный департамент и должность
        [Reactive] public KeyValuePair<int, string> SelectedDepartment { get; set; }
        [Reactive] public KeyValuePair<int, string> SelectedPosition { get; set; }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        #endregion

        public ReactiveCommand<Unit, Unit> EditEmployeeCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenDismiss { get; }
        public ReactiveCommand<Unit, Unit> DismissCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteEmployeeCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }


        //Команда загрузки данных
        public ReactiveCommand<Unit, Unit> LoadCommand { get; }

        //Коллекция сотрудников
        public ObservableCollection<EmployeeDto> Employees { get; set; } = [];

        public EmployeeAllPageViewModel()
        {
            DeleteEmployeeCommand = ReactiveCommand.CreateFromTask(DeleteEmployeeAsync, CanExecSelectedEmployee());
            EditEmployeeCommand = ReactiveCommand.Create(EditEmployee, CanExecSelectedEmployee());
            SaveCommand = ReactiveCommand.CreateFromTask(Save, CanSave());
            BackCommand = ReactiveCommand.Create(Back);
            OpenDismiss = ReactiveCommand.Create(DismissEmployee,CanExecSelectedEmployee());

            DismissCommand = ReactiveCommand.CreateFromTask(DismissEmployeeAsync, CanExecSelectedEmployee());

            //Загрузка данных
            LoadCommand = ReactiveCommand.CreateFromTask(LoadDateBase);
            LoadCommand.Execute().Subscribe();
        }

        private async Task DismissEmployeeAsync()
        {
            var resultDismiss = MessageBox.Show("Вы действительно хотите уволить сотрудника?", "Предупреждение",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (resultDismiss == MessageBoxResult.Yes && DateMissed > HireDate)
            {
                if (await ManagerHttp.EmployeeHttpClient.DismissEmployeeAsync(SelectedEmployee!.EmployeeId, DateMissed))
                {
                    Employees.Remove(SelectedEmployee!);
                    MessageBox.Show("Сотрудник уволен");
                    if (!IsActiveButtonEdit)
                    {
                        Back();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка увольнения сотрудника");
                }
            }
        }


        private async Task DeleteEmployeeAsync()
        {
            var result = MessageBox.Show("Вы действительно хотите удалить данные сотрудника из БД?", "Предупреждение",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (await ManagerHttp.EmployeeHttpClient.DeleteEmployeeAsync(SelectedEmployee!.EmployeeId))
                {
                    Employees.Remove(SelectedEmployee!);
                    MessageBox.Show("Сотрудник удалён");

                    if (!IsActiveButtonEdit)
                    {
                        Back();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка удаления сотрудника из БД");
                }
            }
            else
            {
                Logger.Info("Удаление сотрудника отменено");
            }
        }

        private async Task Save()
        {
            OriginalEmployee = new EmployeeDto
            {
                Id = SelectedEmployee!.Id,
                Name = Name!,
                Surname = Surname!,
                Patronymic = Patronymic,
                DepartmentId = SelectedDepartment.Key,
                PositionId = SelectedPosition.Key,
                HireDate = HireDate
            };

            if (SelectedEmployee!.Equals(OriginalEmployee))
            {
                MessageBox.Show("Данные не изменены");
                Back();
                return;
            }

            SelectedEmployee!.Department = SelectedDepartment.Value;
            SelectedEmployee!.Position = SelectedPosition.Value;

            var employee = new EmployeeDto
            {
                Id = SelectedEmployee!.Id,
                EmployeeId = SelectedEmployee.EmployeeId,
                Name = Name!,
                Surname = Surname!,
                Patronymic = Patronymic,
                DepartmentId = SelectedDepartment!.Key,
                PositionId = SelectedPosition!.Key,
                HireDate = HireDate,
            };

            //Todo: Обновить кэш
            if (await ManagerHttp.EmployeeHttpClient.UpdateEmployeeAsync(employee))
            {
                MessageBox.Show("Сотрудник обновлен");

                // Обновление данных в коллекции
                var existingEmployee = Employees.FirstOrDefault(e => e.Id == employee.Id);
                if (existingEmployee != null)
                {
                    existingEmployee.Name = employee.Name;
                    existingEmployee.Surname = employee.Surname;
                    existingEmployee.Patronymic = employee.Patronymic;
                    existingEmployee.Department = SelectedDepartment.Value;
                    existingEmployee.Position = SelectedPosition.Value;
                    existingEmployee.HireDate = employee.HireDate;

                    // Уведомление интерфейса об изменении данных
                    var index = Employees.IndexOf(existingEmployee);
                    Employees[index] = existingEmployee;
                }
            }
            else
            {
                MessageBox.Show("Ошибка обновления сотрудника");
            }
        }

        private async Task LoadDateBase()
        {
            Logger.Info("Начало выполнения LoadDateBase");
            try
            {
                // Запускаем запросы одновременно
                var employeesTask = ManagerHttp.EmployeeHttpClient.GetEmployeesAsync();
                var departmentsTask = ManagerHttp.DepartmentHttpClient.GetDepartmentsAsync();
                var positionsTask = ManagerHttp.PositionHttpClient.GetPositionsAsync();

                // Дожидаемся завершения всех запросов
                await Task.WhenAll(employeesTask, departmentsTask, positionsTask);

                var employees = employeesTask.Result;
                var departments = departmentsTask.Result;
                var positions = positionsTask.Result;

                // Проверяем, есть ли данные
                if (employees.Any() || departments.Any() || positions.Any())
                {
                    Employees.Clear();

                    ForeachAddCollection(Employees, employees);
                    PositionList = positions.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
                    DepartmentsList = departments.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
                }
                else
                {
                    Logger.Warn("Ошибка загрузки данных: сервер вернул пустые списки");
                    MessageBox.Show("Ошибка загрузки данных: сервер вернул пустые списки", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Ошибка загрузки данных: {ex}");
            }
        }

        private IObservable<bool> CanExecSelectedEmployee()
        {
            return this.WhenAnyValue(vm => vm.SelectedEmployee)
                .Select(selectedEmployee => selectedEmployee != null);
        }

        private IObservable<bool> CanSave()
        {

            return this.WhenAnyValue(
                vm => vm.Name,
                vm => vm.Surname,
                vm => vm.Patronymic,
                vm => vm.SelectedDepartment,
                vm => vm.SelectedPosition,
                vm => vm.HireDate,
                (name, surname, patronymic, department, position, hireDate) =>
                    !string.IsNullOrWhiteSpace(name) &&
                    !string.IsNullOrWhiteSpace(surname) &&
                    !string.IsNullOrWhiteSpace(patronymic) &&
                    department.Key != 0 &&
                    position.Key != 0 &&
                    (SelectedEmployee != null &&
                     (name != SelectedEmployee.Name ||
                      surname != SelectedEmployee.Surname ||
                      patronymic != SelectedEmployee.Patronymic ||
                      department.Key != SelectedEmployee.DepartmentId ||
                      position.Key != SelectedEmployee.PositionId ||
                      hireDate != SelectedEmployee.HireDate))
            );
            ;
        }

    private void DismissEmployee()
    {
        EditEmployee();
        IsDismiss = true;
        IsActiveButtonEdit = false;
        DateMissed = DateTime.Now;
    }

    private void EditEmployee()
        {
            Reset();
            IsOpenWindowEdit = true;
            IsActiveButtonEdit = true;

            SelectedDepartment = DepartmentsList.FirstOrDefault(x => x.Value == SelectedEmployee!.Department);
            SelectedPosition = PositionList.FirstOrDefault(x => x.Value == SelectedEmployee!.Position);

            Name = SelectedEmployee!.Name;
            Surname = SelectedEmployee!.Surname;
            Patronymic = SelectedEmployee.Patronymic;
            HireDate = SelectedEmployee.HireDate;

        }
        private void Back()
        {
            Reset();
            IsOpenWindowAllEmployee = true;
            IsActiveButtonEdit = true;
            SelectedEmployee = null;
            OriginalEmployee = null;
        }
        private void Reset()
        {
            IsOpenWindowEdit = false;
            IsOpenWindowAllEmployee = false;
            IsDismiss = false;
            IsActiveButtonEdit = false;
        }
    }
}
