using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public interface IWorkHourRepository
    {
        Task CreateWorkHourAsync(WorkHourModel workHour);
        Task DeleteWorkHourAsync(Guid id);
        Task<WorkHourModel> GetWorkHourAsync(Guid id);
        Task<IEnumerable<WorkHourModel>> GetWorkHoursAsync(List<Guid> idList);
        Task UpdateWorkHourAsync(WorkHourModel workHour);
    }
}
