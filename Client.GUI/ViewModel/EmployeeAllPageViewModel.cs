using Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;

namespace Client.GUI.ViewModel
{

    public class EmployeeAllPageViewModel : ViewModelBase
    {

        [Reactive] public EmployeeDto? SelectedEmployee { get; set; }
        [Reactive] public bool IsEditCommand { get; set; } = true;
        [Reactive] public bool IsEmployeeCommand { get; set; }

        public ReactiveCommand<Unit,Unit>  EditEmployeeCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteEmployeeCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        [Reactive] public KeyValuePair<int, string>? SelectedDepartment { get; set; }
        public List<KeyValuePair<int, string>> DepartmentsList { get; }

        [Reactive] public KeyValuePair<int, string>? SelectedPosition{ get; set; }
        public List<KeyValuePair<int, string>> PositionList { get; }


        //Коллекция сотрудников
        public ObservableCollection<EmployeeDto> Employees { get; set; } =
        [
            new EmployeeDto { Id = 1, Name = "Иван", Surname = "Иванов", Patronymic = "Иванович", Department = "IT", Position = "Разработчик", HireDate = new DateTime(2020, 5, 10) },
            new EmployeeDto { Id = 2, Name = "Петр", Surname = "Петров", Patronymic = "Петрович", Department = "Бухгалтерия", Position = "Бухгалтер", HireDate = new DateTime(2019, 8, 15) },
            new EmployeeDto { Id = 3, Name = "Алексей", Surname = "Смирнов", Patronymic = "Алексеевич", Department = "Отдел кадров", Position = "HR-менеджер", HireDate = new DateTime(2021, 1, 20) },
            new EmployeeDto { Id = 4, Name = "Мария", Surname = "Кузнецова", Patronymic = "Андреевна", Department = "Продажи", Position = "Менеджер", HireDate = new DateTime(2018, 3, 5) },
            new EmployeeDto { Id = 5, Name = "Ольга", Surname = "Васильева", Patronymic = "Сергеевна", Department = "Финансы", Position = "Аналитик", HireDate = new DateTime(2022, 7, 11) },
            new EmployeeDto { Id = 6, Name = "Дмитрий", Surname = "Морозов", Patronymic = "Владимирович", Department = "IT", Position = "Системный администратор", HireDate = new DateTime(2017, 6, 30) },
            new EmployeeDto { Id = 7, Name = "Анна", Surname = "Новикова", Patronymic = "Игоревна", Department = "Маркетинг", Position = "Маркетолог", HireDate = new DateTime(2023, 2, 14) },
            new EmployeeDto { Id = 8, Name = "Сергей", Surname = "Федоров", Patronymic = "Анатольевич", Department = "Логистика", Position = "Логист", HireDate = new DateTime(2016, 10, 25) },
            new EmployeeDto { Id = 9, Name = "Екатерина", Surname = "Попова", Patronymic = "Дмитриевна", Department = "IT", Position = "QA-инженер", HireDate = new DateTime(2021, 9, 8) },
            new EmployeeDto { Id = 10, Name = "Владимир", Surname = "Киселев", Patronymic = "Артемович", Department = "Бухгалтерия", Position = "Аудитор", HireDate = new DateTime(2015, 12, 3) },
            new EmployeeDto { Id = 11, Name = "Ирина", Surname = "Зайцева", Patronymic = "Романовна", Department = "Юридический отдел", Position = "Юрист", HireDate = new DateTime(2019, 4, 22) },
            new EmployeeDto { Id = 12, Name = "Максим", Surname = "Соловьев", Patronymic = "Евгеньевич", Department = "Отдел безопасности", Position = "Охранник", HireDate = new DateTime(2018, 11, 17) },
            new EmployeeDto { Id = 13, Name = "Александр", Surname = "Макаров", Patronymic = "Александрович", Department = "IT", Position = "Аналитик", HireDate = new DateTime(2020, 6, 12) },
            new EmployeeDto { Id = 14, Name = "Евгений", Surname = "Куликов", Patronymic = "Владимирович", Department = "Продажи", Position = "Менеджер", HireDate = new DateTime(2017, 7, 1) },
            new EmployeeDto { Id = 15, Name = "Анастасия", Surname = "Семенова", Patronymic = "Игоревна", Department = "Финансы", Position = "Бухгалтер", HireDate = new DateTime(2022, 8, 15) },
            new EmployeeDto { Id = 16, Name = "Артем", Surname = "Петухов", Patronymic = "Александрович", Department = "IT",  Position = "Бухгалтер", HireDate = new DateTime(2022, 8, 15) }
        ];

        public EmployeeAllPageViewModel()
        {
            DeleteEmployeeCommand = ReactiveCommand.CreateFromTask(DeleteEmployeeAsync,CanExecSelectedEmployee());
            EditEmployeeCommand = ReactiveCommand.Create(EditEmployee,CanExecSelectedEmployee());
            BackCommand = ReactiveCommand.Create(Back);
            SaveCommand = ReactiveCommand.Create(Save);

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

        }
        //Todo Переделать логику под вызовы из бд
        private async Task DeleteEmployeeAsync()
        {
            //var isDeleted = await ManagerHttp.EmployeeHttpClient.DeleteEmployeeAsync(SelectedEmployee.Id);
            var isDeleted = true;

            if (isDeleted)
            {
                Employees.Remove(SelectedEmployee!);
                SelectedEmployee = null;
                MessageBox.Show("Сотрудник удален");
            }
            else
            {
                MessageBox.Show("Ошибка удаления сотрудника");
            }
        }
        private IObservable<bool> CanExecSelectedEmployee()
        {
            return this.WhenAnyValue(vm => vm.SelectedEmployee)
                .Select(selectedEmployee => selectedEmployee != null);
        }
        private void EditEmployee()
        {
            Reset();
            IsEditCommand = true;
            SelectedDepartment = DepartmentsList.FirstOrDefault(x => x.Value == SelectedEmployee!.Department);
            SelectedPosition = PositionList.FirstOrDefault(x => x.Value == SelectedEmployee!.Position);

        }

        private void Save()
        {
            SelectedEmployee!.Department = SelectedDepartment?.Value ?? string.Empty;
            SelectedEmployee!.Position = SelectedPosition?.Value ?? string.Empty;
        }


        private void Back()
        {
            Reset();
            IsEmployeeCommand = true;
        }
        private void Reset()
        {
            IsEditCommand = false;
            IsEmployeeCommand = false;
        }

    }
}
