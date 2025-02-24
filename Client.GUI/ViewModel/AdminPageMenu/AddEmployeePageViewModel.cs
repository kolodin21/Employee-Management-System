using ReactiveUI;
using System.Reactive;
using System.Windows;
using Models;
using ReactiveUI.Fody.Helpers;
using NLog;
using System.Reactive.Disposables;
using System;

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
        public List<KeyValuePair<int, string>>? DepartmentsList { get; set; } = [];
        public List<KeyValuePair<int, string>>? PositionList { get; set; } = [];
        [Reactive] public KeyValuePair<int, string> SelectedDepartment { get; set; }
        [Reactive] public KeyValuePair<int, string> SelectedPosition { get; set; }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> LoadCommand { get; }


        public AddEmployeePageViewModel()
        {
            SaveCommand = ReactiveCommand.CreateFromTask(Save,CanSave());

            LoadCommand= ReactiveCommand.CreateFromTask(LoadDateBase);

            LoadCommand.Execute().Subscribe(); ;
        }

        private async void Init()
        {
            await LoadDateBase();
        }

        private async Task LoadDateBase()
        {
            try
            {
                Logger.Info("Начало загрузки базы данных...");

                if (ManagerHttp?.DepartmentHttpClient == null || ManagerHttp?.PositionHttpClient == null)
                {
                    Logger.Error("ManagerHttp не инициализирован!");
                    return;
                }

                // Запускаем запросы одновременно
                var departmentsTask = ManagerHttp.DepartmentHttpClient.GetDepartmentsAsync();
                var positionTask = ManagerHttp.PositionHttpClient.GetPositionsAsync();

                // Дожидаемся завершения всех запросов
                await Task.WhenAll(departmentsTask, positionTask);

                var departmentsList = departmentsTask.Result ?? new List<Department>();
                var positionList = positionTask.Result ?? new List<Position>();

                if (positionList.Any() || departmentsList.Any())
                {
                    PositionList.Clear();
                    DepartmentsList.Clear();

                    PositionList.AddRange(positionList.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)));
                    DepartmentsList.AddRange(departmentsList.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)));

                    SelectedPosition = PositionList.FirstOrDefault();
                    SelectedDepartment = DepartmentsList.FirstOrDefault();
                }
                else
                {
                    Logger.Warn("Ошибка загрузки данных: сервер вернул пустые списки");
                    MessageBox.Show("Ошибка загрузки данных: сервер вернул пустые списки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, "Ошибка при загрузке данных");
                MessageBox.Show($"Ошибка при загрузке данных: {e.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                Reset();
                MessageBox.Show("Сотруник успешно добавлен");
            }
            else
            {
                MessageBox.Show("Ошибка добавления сотрудника");
            }
        }
        private void Reset()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Patronymic = string.Empty;
            SelectedDepartment = DepartmentsList.FirstOrDefault();
            SelectedPosition = PositionList.FirstOrDefault();
        }
    }
}
