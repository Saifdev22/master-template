using Identity.API.Data;
using Starter.Application.Identity.Users.Abstractions;

namespace Starter.Infrastructure.Identity.Users.Services
{
    public class UserService() : IUserService
    {
        public Task<string> ConfirmEmailAsync(string userId, string code, string tenant, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> ConfirmPhoneNumberAsync(string userId, string code)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsWithNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityAppUser> GetAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<IdentityAppUser>> GetListAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
