using System.Reactive;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Client.GUI.ViewModel.AdminPageMenu;

public interface IEmployeeForm
{
    public string?  Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public DateTime HireDate { get; set; }
    public List<KeyValuePair<int, string>>? DepartmentsList { get; set; }
    public List<KeyValuePair<int, string>>? PositionList { get; set; }
    public KeyValuePair<int, string> SelectedDepartment { get; set; }
    public KeyValuePair<int, string> SelectedPosition { get; set; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }

}