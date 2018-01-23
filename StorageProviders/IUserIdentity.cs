using Microsoft.AspNetCore.Identity;
using Models.DbModels;

namespace Reactive.UserIdentity
{
    public interface IUserIdentity: IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
    {
    }
}
