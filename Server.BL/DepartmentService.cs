using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Server.DAL.Repository;

namespace Server.BL
{
    public class DepartmentService: ServiceBase
    {
        public async Task<bool> AddDepartmentAsync(string name) =>
            await repositoryManager.DepartmentRepository.AddDepartmentAsync(name);
       

        public async Task UpdateDepartmentAsync(int id, string name) =>
            await repositoryManager.DepartmentRepository.UpdateDepartmentAsync(id, name);


        public async Task DeleteDepartmentAsync(int departmentId) =>
            await repositoryManager.DepartmentRepository.DeleteDepartmentAsync(departmentId);
        
    }
}
