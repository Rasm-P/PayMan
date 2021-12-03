using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public int Status { get; set; } = 404;

        public NotFoundException(Guid id) : base(String.Format("User does not contain Job of Id: {0}", id.ToString())) { }

    }
}
