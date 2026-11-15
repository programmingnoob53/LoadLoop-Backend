using LoadLoop.api.Dtos;
using LoadLoop.api.Helper;
using LoadLoop.api.Models;
using LoadLoop.api.Data;
using Microsoft.EntityFrameworkCore;

namespace LoadLoop.api.Services
{
    public class TruckService
    {
        private readonly LoadLoopContext _dbContext;

        public TruckService(LoadLoopContext dbContext) => _dbContext = dbContext;

    }
}
