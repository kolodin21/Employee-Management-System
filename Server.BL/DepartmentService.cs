using Models;

namespace Server.BL
{
    public class DepartmentService: ServiceBase
    {
        public async Task<bool> AddDepartmentAsync(string name) =>
            await RepositoryManager.DepartmentRepository.AddDepartmentAsync(name);
        
        public async Task<bool> UpdateDepartmentAsync(int id, string name) =>
            await RepositoryManager.DepartmentRepository.UpdateDepartmentAsync(id, name);

        public async Task<IEnumerable<Department>> GetDepartmentsAsync() =>
            await RepositoryManager.DepartmentRepository.GetAllDepartmentsAsync();

        //Todo добавить для поиска по id 

    }
}
