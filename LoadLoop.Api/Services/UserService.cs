using LoadLoop.api.Models;
using LoadLoop.api.Helper;
using LoadLoop.api.Dtos;
using Microsoft.EntityFrameworkCore;
using LoadLoop.api.Data;
namespace LoadLoop.api.Services
{
    public class UserService
    {
        private readonly LoadLoopContext _dbContext;

        public UserService(LoadLoopContext dbContext) => _dbContext = dbContext;

        
    }
}
