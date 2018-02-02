using System.Threading.Tasks;

namespace Reactive.DAL.Interfaces
{
    public interface IUserQueries<TUser>
    {
        Task<bool> CreateAsync(TUser user);
        Task<bool> DeleteAsync(TUser user);
        Task<TUser> FindByIdAsync(string userId);
        Task<TUser> FindByNameAsync(string normalizedUserName);
        Task<bool> UpdateAsync(TUser user);
        Task<TUser> FindByEmailAsync(string normalizedEmail);
    }
}
