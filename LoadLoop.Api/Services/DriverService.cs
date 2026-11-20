using LoadLoop.api.Data;
using LoadLoop.api.Dtos;
using LoadLoop.api.Helper;
using LoadLoop.api.Models;
using Microsoft.EntityFrameworkCore;

namespace LoadLoop.api.Services
{
    public class DriverService
    {
        private  readonly LoadLoopContext _dbContext;

        public DriverService(LoadLoopContext _dbcontext) => _dbContext = _dbcontext;
        //GET
        public async Task<List<GetDriverDto>> GetDriversAsync()
        {
            List<UserTable> list = await _dbContext.UserTables.AsNoTracking().Where(t => t.UsRole == "D").ToListAsync();//get the drivers
            //if the table is not empty
            List<GetDriverDto> driverDtos = new List<GetDriverDto>(list.Count);
            if (list.Any())
            {
                for(int k = 0; k < list.Count; k++)
                {
                    var user = list[k];
                    //get the corresponding driver entity
                    var driver = await _dbContext.DriverTables.AsNoTracking().FirstOrDefaultAsync(t => t.UsId == user.UsId);
                    if(driver is null) continue;//skip an iteration
                    GetDriverDto driverDto = new GetDriverDto
                    {
                        driverID = driver.DriId,
                        userEmail = user.UsEmail,
                        userName = user.UsName,
                        userSurname = user.UsSurname,
                        phoneNumber = user.UsPhone,
                        money = driver.DriSalary,
                        isAvailable = driver.IsAvailable,
                    };
                    driverDtos.Add(driverDto);
                }
            }
            return driverDtos;
        }
        public async Task<GetDriverDto?> GetDriverByIDAsync(int id)
        {
            var driver = await _dbContext.DriverTables.AsNoTracking().Where(t => t.DriId == id).SingleOrDefaultAsync();
            if(driver is null) return null;
            var user = await _dbContext.UserTables.AsNoTracking().FirstOrDefaultAsync(t => t.UsId == driver.UsId);
            if(user is null) return null;

            GetDriverDto driverDto = new GetDriverDto
            {
                driverID = driver.DriId,
                userEmail = user.UsEmail,
                userName = user.UsName,
                userSurname = user.UsSurname,
                phoneNumber = user.UsPhone,
                money = driver.DriSalary,
                isAvailable = driver.IsAvailable,
            };
            return driverDto;
        }

        public async Task<GetDriverDto?> GetDriverByEmailAsync(string email)
        {
            var user = await _dbContext.UserTables.AsNoTracking().FirstOrDefaultAsync(t => t.UsEmail == email);
            if(user is null) return null;
            var driver = await _dbContext.DriverTables.AsNoTracking().FirstOrDefaultAsync(t => t.UsId == user.UsId);
            if(driver is null) return null;

            GetDriverDto driverDto = new GetDriverDto
            {
                driverID = driver.DriId,
                userEmail = user.UsEmail,
                userName = user.UsName,
                userSurname = user.UsSurname,
                phoneNumber = user.UsPhone,
                money = driver.DriSalary,
                isAvailable = driver.IsAvailable,
            };
            return driverDto;
        }
        //POST
        
        public async Task<OperationalResults> CreateDriverAsync(CreateDriverDto driverDto)
        {
            try
            {
                // Check whether the user already exists
                var user = await _dbContext.UserTables
                    .FirstOrDefaultAsync(u => u.UsEmail == driverDto.userEmail);

                if (user is not null)
                    return OperationalResults.AlreadyExists;

                // Create user
                var userTable = new UserTable
                {
                    UsName = driverDto.name, // Assuming your DTO has a name property
                    UsIdNumber = driverDto.userIdNumber,
                    UsEmail = driverDto.userEmail,
                    UsPhone = driverDto.phoneNumber,
                    UsPasswordHash = HashAlgo.hashPassword(driverDto.password),
                    UsRole = "D",
                    UsSurname = driverDto.surname
                };

                await _dbContext.UserTables.AddAsync(userTable);

                // Save user to generate its identity ID
                await _dbContext.SaveChangesAsync();

                // Create driver using generated user ID
                var driver = new DriverTable
                {
                    UsId = userTable.UsId, // This is returned by EF after saving
                    DriSalary = driverDto.money,
                    IsAvailable = driverDto.isAvailable
                };

                await _dbContext.DriverTables.AddAsync(driver);
                await _dbContext.SaveChangesAsync();

                return OperationalResults.Success;
            }
            catch (DbUpdateException)
            {
                return OperationalResults.Failure;
            }
        }

        //PUT
        public async Task<OperationalResults> UpdateDriverAsync(int driverID, UpadateDriverDto updateDriverDto)
        {
            try
            {
                //Get the driver entity
                var driver = await _dbContext.DriverTables.FindAsync(driverID);
                if(driver is null) return OperationalResults.NotFound;
                //Get the user
                var user = await _dbContext.UserTables.FirstOrDefaultAsync(t => t.UsId == driver.UsId);
                if(user is null) return OperationalResults.NotFound;

                // Update the driver entity
                driver.DriSalary = updateDriverDto.money;
                driver.IsAvailable = updateDriverDto.isAvailable;
                // Update the user entity
                user.UsEmail = updateDriverDto.userEmail;
                user.UsName = updateDriverDto.userName;
                user.UsPasswordHash = HashAlgo.hashPassword(updateDriverDto.newPassword);
                user.UsSurname = updateDriverDto.userSurname;
                user.UsPhone = updateDriverDto.phoneNumber;

                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;
            }
            catch (DbUpdateException)
            {
                return OperationalResults.Failure;
            }
        }
        //DELETE
        public async Task<OperationalResults> DeleteDriverByIDAsync(int driverId)
        {
            try
            {
                // Checking if the user exists
                var driver = await _dbContext.DriverTables.FindAsync(driverId);
                if(driver is null) return OperationalResults.NotFound;
                // Get the list of trucks
                var listTrucks = await _dbContext.TruckTables.Where(t => t.DriId == driverId).ToListAsync();
                // Get the corresponding user identity
                var user = await _dbContext.UserTables.FirstOrDefaultAsync(u => u.UsId == driver.UsId);
                if(user is null) return OperationalResults.NotFound;
                //delete all the trucks
                if (listTrucks.Any())
                {
                    _dbContext.TruckTables.RemoveRange(listTrucks);
                }
                //remove the entities
                _dbContext.UserTables.Remove(user);
                _dbContext.DriverTables.Remove(driver);

                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;

            }
            catch (DbUpdateException)
            {
                return OperationalResults.Failure;
            }
        }

        public async Task<OperationalResults> DeleteDriverByEmailAsync(string email)
        {
            try
            {
                 var user = await _dbContext.UserTables.FirstOrDefaultAsync(t => t.UsEmail == email);
                if(user is null) return OperationalResults.NotFound;
                var driver = await _dbContext.DriverTables.FirstOrDefaultAsync(d => d.UsId == user.UsId);
                if(driver is null) return OperationalResults.NotFound;
                // At this point there is a driver
                var listTrucks = await _dbContext.TruckTables.Where(t => t.DriId == driver.DriId).ToArrayAsync();
                if (listTrucks.Any())
                {
                    _dbContext.TruckTables.RemoveRange(listTrucks);
                }
                _dbContext.DriverTables.Remove(driver);//remove the reference first
                _dbContext.UserTables.Remove(user);

                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;
            }
            catch(DbUpdateException)
            {
                return OperationalResults.Failure;
            }
        }
    }
}