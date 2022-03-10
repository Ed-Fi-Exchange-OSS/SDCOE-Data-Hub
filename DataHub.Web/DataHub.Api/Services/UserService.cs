using System.Threading.Tasks;
using AutoMapper;
using DataHub.Api.Data;
using DataHub.Api.Data.Models;

namespace DataHub.Api.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUser(int userId);
    }

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly SDCOEDatahubContext _context;

        public UserService(IMapper mapper, SDCOEDatahubContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserDto> GetUser(int userId)
        {
            var user = await _context.User
                .FindAsync(userId).ConfigureAwait(false);
            return _mapper.Map<UserDto>(user);
        }
    }
}