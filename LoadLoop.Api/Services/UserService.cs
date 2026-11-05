using LoadLoop.api.Models;
using LoadLoop.api.Helper;
using LoadLoop.api.Dtos;
using Microsoft.EntityFrameworkCore;
namespace LoadLoop.api.Services
{
    //for now we have one User service so no need for an interface
    public class UserService
    {
        private readonly LoadLoopDbContext _dbContext;

        public UserService(LoadLoopDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<GetUserDto>> GetUsersAsync()
        {
            //asNoTracking => this is a readonly query
            return 
                await _dbContext.UserTables.Select(m => new GetUserDto {
                Id = m.UserId, age = m.UserAge, Email =  m.UserEmail,UserRole = m.UserRole, UserName = m.UserName,
                }).AsNoTracking().ToListAsync();
     
        }
        public async Task<List<GetTruckDto>> GetTrucksByUserID(int id)
        {
            return await _dbContext
                .Trucks.Where(t => t.UserId == id)
                .Select(t => new GetTruckDto
                {
                    Id = t.UserId,
                    ImageURL = t.TruckImage,
                    LicensePlate = t.TruckLicensePlate,
                    UserId = t.UserId,
                    TruckCapacity = t.TruckCapacity,
                }).ToListAsync();
        }
        public async Task<GetUserDto?> GetUserByIDAsync(int id)
        {
            return await _dbContext.UserTables
                .AsNoTracking()
                .Where(m => m.UserId == id)
                .Select(m =>
            new GetUserDto
            {
                Id = m.UserId,
                age = m.UserAge,
                Email = m.UserEmail,
                UserRole = m.UserRole,
                UserName = m.UserName,
            }).FirstOrDefaultAsync();
        }

        public async Task<GetUserDto?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.UserTables
                .AsNoTracking()
                .Where(m => m.UserEmail.Equals(email))
                .Select(m => new GetUserDto {Id = m.UserId, UserName = m.UserName, age = m.UserAge, Email = m.UserEmail,UserRole = m.UserRole })
                .FirstOrDefaultAsync();
        }

        //PUT methods
        public async Task<OperationalResults> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _dbContext.UserTables.FindAsync(id);
            if (user == null) return OperationalResults.NotFound;

            user.UserAge = dto.UserAge ?? user.UserAge;
            user.UserName = dto.UserName ?? user.UserName;

            if (!string.IsNullOrWhiteSpace(dto.UserPassword))
                user.UserPasswordHash = HashAlgo.hashPassword(dto.UserPassword);

            if (!string.IsNullOrWhiteSpace(dto.UserEmail) && dto.UserEmail != user.UserEmail)
            {
                if (await _dbContext.UserTables.AnyAsync(m => m.UserEmail == dto.UserEmail))
                    return OperationalResults.AlreadyExists;   // nothing saved yet

                user.UserEmail = dto.UserEmail;
            }

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

        //DELETE methods

        public async Task<OperationalResults> deleteUserByIDAsync(int id)
        {
            var user = await _dbContext.UserTables.FindAsync(id);

            if (user == null) return OperationalResults.NotFound;

            var trucks = await _dbContext.Trucks.Where(t => t.UserId == id).ToListAsync();
            _dbContext.Trucks.RemoveRange(trucks);//delete trucks belonging to user
            _dbContext.UserTables.Remove(user);//delete the user

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
        public async Task<OperationalResults> deleteUserByEmailAsync(string email)
        {
            var id = await _dbContext.UserTables
            .Where(u => u.UserEmail == email)
            .Select(u => (int?)u.UserId)
            .FirstOrDefaultAsync();

            if (id == null) return OperationalResults.NotFound;

            return await deleteUserByIDAsync(id.Value);
        }

        //POST method(s)

        public async Task<OperationalResults> registerUser(CreateUserDto userDto)
        {
            var user = await _dbContext.UserTables
                .Where(m => m.UserEmail == userDto.UserEmail).FirstOrDefaultAsync();
            if (user != null)
                return OperationalResults.AlreadyExists;//user already exists

            UserTable userEntity = new UserTable
            {
                UserEmail = userDto.UserEmail,
                UserAge = userDto.UserAge,
                UserRole = userDto.UserRole.ToString(),
                UserName = userDto.UserName,
                UserPasswordHash = HashAlgo.hashPassword(userDto.Password)
            };

            await _dbContext.UserTables.AddAsync(userEntity);

            try
            {
                await _dbContext.SaveChangesAsync();
                return OperationalResults.Success;
            }
            catch (Exception)
            {
                return OperationalResults.Failiure;
            }
        }
    }
}
