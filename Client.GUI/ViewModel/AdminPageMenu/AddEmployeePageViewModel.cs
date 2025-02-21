using ReactiveUI;
using System.Reactive;
using System.Windows;
using Models;
using ReactiveUI.Fody.Helpers;
using NLog;

namespace Client.GUI.ViewModel.AdminPageMenu
{
    public class AddEmployeePageViewModel : ViewModelBase, IEmployeeForm
    {
        //Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Reactive] public string Name { get; set; } = "Колодин";
        [Reactive] public string Surname { get; set; } = "Александр";
        [Reactive] public string? Patronymic { get; set; } = "Владимирович";
        [Reactive] public DateTime HireDate { get; set; } = DateTime.Now;
        public List<KeyValuePair<int, string>> DepartmentsList { get; set; }
        public List<KeyValuePair<int, string>> PositionList { get; set; }
        [Reactive] public KeyValuePair<int, string> SelectedDepartment { get; set; }
        [Reactive] public KeyValuePair<int, string> SelectedPosition { get; set; }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> LoadCommand { get; }



        public AddEmployeePageViewModel()
        {
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

            SaveCommand = ReactiveCommand.CreateFromTask(Save,CanSave());

            LoadCommand= ReactiveCommand.CreateFromTask(LoadDateBase);
            LoadCommand.Execute().Subscribe();
        }
       


        private async Task LoadDateBase()
        {
            try
            {
                // Запускаем запросы одновременно
                var departmentsTask = ManagerHttp.DepartmentHttpClient.GetDepartmentsAsync();
                var positionTask = ManagerHttp.PositionHttpClient.GetPositionsAsync();

                // Дожидаемся завершения всех запросов
                await Task.WhenAll(departmentsTask, positionTask);

                var departmentsList = departmentsTask.Result;
                var positionList = positionTask.Result;

                if (positionList.Any() || departmentsList.Any())
                {
                    PositionList.Clear();
                    DepartmentsList.Clear();
                    PositionList = positionList.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
                    DepartmentsList = departmentsList.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();

                    SelectedPosition = PositionList.FirstOrDefault();
                    SelectedDepartment = DepartmentsList.FirstOrDefault();
                }
                else
                {
                    Logger.Warn("Ошибка загрузки данных: сервер вернул пустые списки");
                    MessageBox.Show("Ошибка загрузки данных: сервер вернул пустые списки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                Logger.Error($"Ошибка загрузки данных: {ex}");
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private IObservable<bool> CanSave()
        {
            return this.WhenAnyValue(
                x => x.Name,
                x => x.Surname,
                x => x.Patronymic,
                x => x.SelectedDepartment,
                x => x.SelectedPosition,
                x => x.HireDate,
                (name, surname, patronymic, selectedDepartment, selectedPosition, hireDate) =>
                    !string.IsNullOrWhiteSpace(name) &&
                    !string.IsNullOrWhiteSpace(surname) &&
                    !string.IsNullOrWhiteSpace(patronymic) &&
                    selectedDepartment.Key != 0 &&
                    selectedPosition.Key != 0 &&
                    hireDate != DateTime.MinValue);
        }

        private async Task Save()
        {
            //Todo добавить проверку на уникальность

            var employee = new Employee
            {
                Name = Name,
                Surname = Surname,
                Patronymic = Patronymic,
                DepartmentId = SelectedDepartment.Key,
                PositionId = SelectedPosition.Key,
                HireDate = HireDate,
                DateOfDismissal = null
            };

            if (await ManagerHttp.EmployeeHttpClient.AddEmployeeAsync(employee))
            {
                Logger.Info($"Новый сотрудник {Name} {Surname} {Patronymic} успешно добавлен");
                MessageBox.Show("Сотруник успешно добавлен");
            }
            else
            {
                Logger.Warn($"Ошибка добавления сотрудника {Name} {Surname} {Patronymic}");
                MessageBox.Show("Ошибка добавления сотрудника");
            }
        }
    }
}
