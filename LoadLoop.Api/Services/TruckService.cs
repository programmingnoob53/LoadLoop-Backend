using LoadLoop.api.Dtos;
using LoadLoop.api.Helper;
using LoadLoop.api.Models;
using LoadLoop.api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

namespace LoadLoop.api.Services
{
    public class TruckService
    {
        private readonly LoadLoopContext _dbContext;

        public TruckService(LoadLoopContext dbContext) => _dbContext = dbContext;

        //GET
        public async Task<List<GetTruckDto>> GetTrucksAsync()
        {
            //get all the trucks
            return await _dbContext.TruckTables.Select(m => new GetTruckDto
            {
                truckID = m.TruId,
                truckImage = m.TruImage,
                truckMaxweight = m.TruMaxWeight,
                truckLicensePlate = m.TruLicensePlate,
                truckType = m.TruType
            }).AsNoTracking().ToListAsync();
        }

        public async Task<List<GetTruckDto>> GetTrucksByUserIDAsync(int userID)
        {
            return await _dbContext.TruckTables.Where(t=> t.DriId == userID).Select(t => new GetTruckDto
            {
                truckID = t.TruId,
                truckImage = t.TruImage,
                truckMaxweight = t.TruMaxWeight,
                truckLicensePlate = t.TruLicensePlate,
                truckType = t.TruType
            }).AsNoTracking().ToListAsync();
        }

        public async Task<GetTruckDto?> GetTruckByIDAsync(int truckID)
        {
            return await _dbContext.TruckTables.Where(t => t.TruId == truckID).Select(t => new GetTruckDto
            {
                truckID = t.TruId,
                truckImage = t.TruImage,
                truckMaxweight = t.TruMaxWeight,
                truckLicensePlate = t.TruLicensePlate,
                truckType = t.TruType
            }).AsNoTracking().SingleOrDefaultAsync();
        }

        //POST
        public async Task<OperationalResults> CreateTruckAsync(CreateTruckDto truckDto)
        {
            var user = await _dbContext.DriverTables.Where(u => u.DriId == truckDto.driverID).FirstOrDefaultAsync();
            if(user == null)
            {
                return OperationalResults.NotFound;//user does not exist or not a driver
            }

            var newTruck = new TruckTable
            {
                DriId = truckDto.driverID,
                TruImage = truckDto.truckImage,
                TruLicensePlate = truckDto.truckLicensePlate,
                TruMaxWeight = truckDto.truckMaxWeight,
                TruType = truckDto.truckType
            };

            await _dbContext.TruckTables.AddAsync(newTruck);

            try
            {
                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;
            }
            catch(DbUpdateException)
            {
                //database error
                return OperationalResults.Failure;
            }
        }
        //PUT
        public async Task<OperationalResults> UpdateTruckAsyc(int id, UpdateTruckDto truckDto)
        {
            var truck = await _dbContext.TruckTables.FindAsync(id);

            if(truck == null) return OperationalResults.NotFound;

            truck.TruMaxWeight = truckDto.truckMaxWeight;
            truck.TruType = truckDto.truckType;

            try
            {
                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;
            }
            catch (DbUpdateException)
            {
                return OperationalResults.Failure;
            }
            
        }

        //DELETE
        public async Task<OperationalResults> DeleteTruckAsync(int id)
        {
            var truck = await _dbContext.TruckTables.FindAsync(id);

            if(truck == null) return OperationalResults.NotFound;

            _dbContext.TruckTables.Remove(truck);

            try
            {
                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;
            }
            catch (DbUpdateException)
            {
                return OperationalResults.Failure;
            }
        }

        public async Task<OperationalResults> DeleteTrucksByUserIDAsync(int id)
        {
            var truckList = await _dbContext.TruckTables.Where(t => t.DriId == id).ToListAsync();

            if (truckList.Any())
            {
                _dbContext.RemoveRange(truckList);
            }
            else
            {
                return OperationalResults.NotFound;//invalid role or no trucks yet
            }

             try
            {
                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;
            }
            catch (DbUpdateException)
            {
                return OperationalResults.Failure;
            }
        }
        //PATCH
        public async Task<OperationalResults> PatchTruck(int id, PatchTruckDto truckDto)
        {
            try
            {
                var truck = await _dbContext.TruckTables.FindAsync(id);
                if (truck is null) return OperationalResults.NotFound;
                truck.TruType = truckDto.truckType ?? truck.TruType;
                truck.TruMaxWeight = truckDto.truckMaxWeight ?? truck.TruMaxWeight;

                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;
            }
            catch (DbUpdateException)
            {
                return OperationalResults.Failure;
            }
        }

    }
}
