namespace Server.BL
{
    public class DepartmentService: ServiceBase
    {
        public async Task<bool> AddDepartmentAsync(string name) =>
            await repositoryManager.DepartmentRepository.AddDepartmentAsync(name);
       

        public async Task<bool> UpdateDepartmentAsync(int id, string name) =>
            await repositoryManager.DepartmentRepository.UpdateDepartmentAsync(id, name);


        public async Task<bool> DeleteDepartmentAsync(int departmentId) =>
            await repositoryManager.DepartmentRepository.DeleteDepartmentAsync(departmentId);
        
    }
}
