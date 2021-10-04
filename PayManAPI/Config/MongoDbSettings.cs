using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Config
{
    public class MongoDbSettings
    {
        public String Host { get; set; }
        public int Port { get; set; }

        public String ConnectionString { get { return $"mongodb://{Host}:{Port}"; } }
    }
}
