using System;
using System.Collections.Generic;
using System.Text;

namespace Reactive.DAL.Interfaces
{
    public interface IDbClient<TClient>
    {
        TClient Client
        {
            get;
        }
    }
}
