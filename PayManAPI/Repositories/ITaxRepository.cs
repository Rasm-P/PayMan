using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public interface ITaxRepository
    {
        Task CreateTaxAsync(TaxModel tax);
        Task DeleteTaxAsync(Guid id);
        Task<TaxModel> GetTaxAsync(Guid id);
        Task<IEnumerable<TaxModel>> GetTaxsAsync(List<Guid> idList);
        Task UpdateTaxAsync(TaxModel tax);
    }
}
