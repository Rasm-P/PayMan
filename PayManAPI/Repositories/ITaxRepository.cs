using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public interface ITaxRepository
    {
        Task<IEnumerable<TaxModel>> GetTaxesFromIdListAsync(List<Guid> idList);
    }
}
