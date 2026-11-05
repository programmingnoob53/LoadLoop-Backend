using LoadLoop.api.Dtos;
using LoadLoop.api.Helper;
using LoadLoop.api.Models;
using Microsoft.EntityFrameworkCore;

namespace LoadLoop.api.Services
{
    public class TruckService
    {
        private readonly LoadLoopDbContext _dbContext;

        public TruckService(LoadLoopDbContext dbContext) => _dbContext = dbContext;

        //GET
        public async Task<List<GetTruckDto>> GetTrucksAsync()
        {
            return await _dbContext.Trucks.Select(m => new GetTruckDto
            {
                Id = m.TruckId,
                ImageURL = m.TruckImage,
                LicensePlate = m.TruckLicensePlate,
                UserId = m.UserId,
                TruckCapacity = m.TruckCapacity,
            }).ToListAsync();
        }

        public async Task<List<GetTruckDto>> GetTrucksByUserIDAsync(int id)
        {
            return await _dbContext
                .Trucks.Where(t => t.UserId == id)
                .Select(t => new GetTruckDto
                {
                    Id = t.UserId,
                    ImageURL = t.TruckImage,
                    LicensePlate = t.TruckLicensePlate,
                    UserId= t.UserId,
                    TruckCapacity = t.TruckCapacity
                }).ToListAsync();
        }
        public async Task<GetTruckDto?> GetTruckByIDAsync(int id)
        {
            return await _dbContext.Trucks
                .Where(t => t.TruckId  == id)
                .Select(t => new GetTruckDto
                {
                    Id = t.TruckId,
                    ImageURL = t.TruckImage,
                    LicensePlate = t.TruckLicensePlate,
                    UserId = t.UserId,
                    TruckCapacity = t.TruckCapacity
                   
                })
                .FirstOrDefaultAsync();
        }

        //POST
        public async Task<OperationalResults> CreateTruckAsync(CreateTruckDto truckDto)
        {
            //check if the user exists first
            var user = await _dbContext.UserTables
                .Where(u => u.UserEmail == truckDto.UserEmail)
                .FirstOrDefaultAsync();

            if(user == null)
            {
                return OperationalResults.NotFound;//the user does not exists
            }

            if(user.UserRole != "T")
            {
                return OperationalResults.InvalidRole;//only a transport provider can add a truck
            }
            //create a truck entity to store
            Truck truck = new Truck
            {
                TruckImage = truckDto.TruckImage,
                TruckLicensePlate = truckDto.LicensePlate,
                TruckCapacity = truckDto.TruckCapacity,
                UserId = user.UserId//link the user
            };

            await _dbContext.Trucks.AddAsync(truck);

            try
            {
                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;
            }
            catch (DbUpdateException)
            {
                return OperationalResults.Failiure;
            }
        }

        //PUT
        public async Task<OperationalResults> UpdateTruckAsync(int id, UpdateTruckDto truckDto)
        {
            var truck = await _dbContext
                .Trucks.FindAsync(id);
            if(truck == null) return OperationalResults.NotFound;

            truck.TruckLicensePlate = truckDto.LicensePlate ?? truck.TruckLicensePlate;
            truck.TruckImage = truckDto.ImageURL ?? truck.TruckImage;
            truck.TruckCapacity = truckDto.TruckCapacity == 0 ? truck.TruckCapacity : truckDto.TruckCapacity;

            try
            {
                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;
            }
            catch (DbUpdateException) { 
                return OperationalResults.Failiure;
            }
        }

        //DELETE
        public async Task<OperationalResults> deleteTruckByIDAsysnc(int id)
        {
            var truck = await _dbContext.Trucks.FindAsync(id);

            if (truck == null) return OperationalResults.NotFound;

            _dbContext.Trucks.Remove(truck);

            try
            {
                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;
            }
            catch (DbUpdateException)
            {
                return OperationalResults.Failiure;
            }
        }

        public async Task<OperationalResults> deleteTrucksByUserAsync(int userId)
        {
      
            List<Truck> list = await _dbContext.Trucks.Where(t => t.UserId == userId).ToListAsync();
            if (list.Any())
                _dbContext.Trucks.RemoveRange(list);
            else 
                return OperationalResults.NotFound;

            try
            {
                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;
            }
            catch (DbUpdateException)
            {
                return OperationalResults.Failiure;
            }
        }

    }
}
