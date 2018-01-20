using System;
using System.Collections.Generic;
using System.Text;

namespace StorageProviders.CosmosDb
{
    public class ApplicationRole
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
