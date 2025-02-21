using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
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
        [Reactive] public string Name { get; set; }
        [Reactive] public string Surname { get; set; }
        [Reactive] public string? Patronymic { get; set; }
        [Reactive] public DateTime HireDate { get; set; }

        public List<KeyValuePair<int, string>> DepartmentsList { get; set; }
        public List<KeyValuePair<int, string>> PositionList { get; set; }

        //Выбранный департамент и должность
        [Reactive] public KeyValuePair<int, string> SelectedDepartment { get; set; }
        [Reactive] public KeyValuePair<int, string> SelectedPosition{ get; set; }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        #endregion

        public ReactiveCommand<Unit,Unit>  EditEmployeeCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteEmployeeCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        //Команда загрузки данных
        public ReactiveCommand<Unit, Unit> LoadCommand { get; }


        //Коллекция сотрудников
        public ObservableCollection<EmployeeDto> Employees { get; set; } = [];

        public EmployeeAllPageViewModel()
        {
            DeleteEmployeeCommand = ReactiveCommand.CreateFromTask(DeleteEmployeeAsync,CanExecSelectedEmployee());
            EditEmployeeCommand = ReactiveCommand.Create(EditEmployee,CanExecSelectedEmployee());
            SaveCommand = ReactiveCommand.CreateFromTask(Save, CanSave());
            BackCommand = ReactiveCommand.Create(Back);

            //Todo : Переделать логику под вызовы из бд
            DepartmentsList = new List<KeyValuePair<int, string>>
            {
                new(1, "IT"),
                new(2, "Бухгалтерия"),
                new(3, "Отдел кадров"),
                new(4, "Продажи"),
                new(5, "Финансы"),
                new(6, "Маркетинг"),
                new(7, "Логистика"),
                new(8, "Юридический отдел"),
                new(9, "Отдел безопасности")
            };
            PositionList = new List<KeyValuePair<int, string>>
            {
                new(1, "Разработчик"),
                new(2, "Бухгалтер"),
                new(3, "HR-менеджер"),
                new(4, "Менеджер"),
                new(5, "Аналитик"),
                new(6, "Системный администратор"),
                new(7, "Маркетолог"),
                new(8, "Логист"),
                new(9, "QA-инженер"),
                new(10, "Аудитор"),
                new(11, "Юрист"),
                new(12, "Охранник"),
                new(13, "Аналитик"),
                new(14, "Бухгалтер"),
            };

            // Подписка на исключения, выбрасываемые командой LoadCommand
            LoadCommand = ReactiveCommand.CreateFromTask(LoadDateBase);

            LoadCommand.ThrownExceptions.Subscribe(ex =>
            {
                Logger.Error(ex, "Ошибка в LoadCommand");
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            });
            //LoadCommand.Execute().Subscribe();

        }

        //Todo Переделать логику под вызовы из бд
        private async Task DeleteEmployeeAsync()
        {
            var result = MessageBox.Show("Вы действительно хотите уволить сотрудника?","Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (await ManagerHttp.EmployeeHttpClient.DeleteEmployeeAsync(SelectedEmployee.Id))
                {
                    Logger.Info($"Сотрудник c {SelectedEmployee!.Id} уволен");
                    Employees.Remove(SelectedEmployee!);
                    MessageBox.Show("Сотрудник уволен");

                    if (!IsActiveButtonEdit)
                    {
                        Back();
                    }
                }
                else
                {
                    Logger.Error($"Ошибка уволенения сотрудника с {SelectedEmployee!.Id}");
                    MessageBox.Show("Ошибка уволенения сотрудника");
                }
            }
            else
            {
                Logger.Info("Уволенение сотрудника отменено");
            }
        }
        private async Task Save()
        {
            if(SelectedEmployee!.Equals(OriginalEmployee))
            {
                MessageBox.Show("Данные не изменены");
                Back();
                return;
            }

            SelectedEmployee!.Department = SelectedDepartment.Value;
            SelectedEmployee!.Position = SelectedPosition.Value;

            var employee = new Employee
            {
                Id = SelectedEmployee!.Id,
                Name = SelectedEmployee!.Name,
                Surname = SelectedEmployee!.Surname,
                Patronymic = SelectedEmployee!.Patronymic,
                DepartmentId = SelectedDepartment!.Key,
                PositionId = SelectedPosition!.Key,
                HireDate = SelectedEmployee!.HireDate,
            };

            //Todo: Обновить кэш
            if (await ManagerHttp.EmployeeHttpClient.UpdateEmployeeAsync(employee))
            {
                Logger.Info($"Сотрудник с {employee.Id} обновлен");
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
                Logger.Error($"Ошибка обновления сотрудника с {employee.Id}");
                MessageBox.Show("Ошибка обновления сотрудника");
            }
        }
        private async Task LoadDateBase()
        {
            //Todo : Сделать кэш в сервисах и обращаться к нему если есть данные
            var employees = await ManagerHttp.EmployeeHttpClient.GetEmployeesAsync();
            //var departments = await ManagerHttp.DepartmentHttpClient.GetDepartmentsAsync();
            //var positions = await ManagerHttp.PositionHttpClient.GetPositionsAsync();


           // PositionList = positions.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
            // DepartmentsList = departments.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();

            Employees.Clear();

            foreach (var item in employees)
            {
                Employees.Add(item);
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
                    (OriginalEmployee != null &&
                     (name != OriginalEmployee.Name ||
                      surname != OriginalEmployee.Surname ||
                      patronymic != OriginalEmployee.Patronymic ||
                      department.Key != OriginalEmployee.DepartmentId ||
                      position.Key != OriginalEmployee.PositionId ||
                      hireDate != OriginalEmployee.HireDate))
            ); ;
        }

        private void EditEmployee()
        {
            Reset();
            IsOpenWindowEdit = true;

            IsActiveButtonEdit = false;

            SelectedDepartment = DepartmentsList.FirstOrDefault(x => x.Value == SelectedEmployee!.Department);
            SelectedPosition = PositionList.FirstOrDefault(x => x.Value == SelectedEmployee!.Position);

            Name = SelectedEmployee!.Name;
            Surname = SelectedEmployee!.Surname;
            Patronymic = SelectedEmployee.Patronymic;
            HireDate = SelectedEmployee.HireDate;

            //Todo НЕ работает проверка на изменение данных, кнопка не правильно активируется
            // Сохранение исходного состояния
            OriginalEmployee = new EmployeeDto
            {
                Id = SelectedEmployee.Id,
                Name = SelectedEmployee.Name,
                Surname = SelectedEmployee.Surname,
                Patronymic = SelectedEmployee.Patronymic,
                DepartmentId = SelectedEmployee.DepartmentId,
                Department = SelectedEmployee.Department,
                PositionId = SelectedEmployee.PositionId,
                Position = SelectedEmployee.Position,
                HireDate = SelectedEmployee.HireDate
            };

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
        }
    }
}
