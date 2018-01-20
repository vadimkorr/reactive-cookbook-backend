using System;
using System.Collections.Generic;
using System.Text;

namespace StorageProviders
{
    public interface IDbClient<TClient>
    {
        TClient Client
        {
            get;
        }
    }
}
