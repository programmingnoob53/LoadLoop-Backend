using LoadLoop.api.Models;
using LoadLoop.api.Helper;
using LoadLoop.api.Dtos;
using Microsoft.EntityFrameworkCore;
using LoadLoop.api.Data;
namespace LoadLoop.api.Services
{
    //this service will work for only normal user of the website
    public class UserService
    {
        private readonly LoadLoopContext _dbContext;

        public UserService(LoadLoopContext dbContext) => _dbContext = dbContext;

        //GET
        public async Task<List<GetUserDto>> GetUsersAsync()
        {
            //we will not return our database admims to the public
            return await _dbContext.UserTables.Where(u => u.UsRole == "U").Select(u => new GetUserDto
            {
                userID = u.UsId,
                userEmail = u.UsEmail,
                userName = u.UsName,
                userSurname = u.UsSurname,
                phoneNumber = u.UsPhone,
            }).AsNoTracking().ToListAsync();
        }

        public async Task<GetUserDto?> GetUserByEmail(string email)
        {
            //read only query
            return await _dbContext.UserTables.Where(t => t.UsEmail == email && t.UsRole == "U").Select(t => new GetUserDto
            {
                userID = t.UsId,
                userEmail = t.UsEmail,
                userName = t.UsName,
                userSurname = t.UsSurname,
                phoneNumber = t.UsPhone
            }).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<GetUserDto?> GetUserByIDAsync(int id)
        {
            return await _dbContext.UserTables.Where(u => u.UsRole == "U" && u.UsId == id).Select(u => new GetUserDto
            {
                userID = u.UsId,
                userEmail = u.UsEmail,
                userName = u.UsName,
                userSurname = u.UsSurname,
                phoneNumber = u.UsPhone
            }).AsNoTracking().FirstOrDefaultAsync();
        }

        //POST
        public async Task<OperationalResults> CreateUserAsync(CreateUserDto userDto)
        {

            var checkUser = await _dbContext.UserTables.FirstOrDefaultAsync(t => t.UsEmail == userDto.userEmail);
            var checkUser2 = await _dbContext.UserTables.FirstOrDefaultAsync(t => t.UsPhone == userDto.phoneNumber);
            var checkUser3 = await _dbContext.UserTables.FirstOrDefaultAsync(t => t.UsIdNumber == userDto.userIdNumber);
            //if not found
            if(checkUser == null && checkUser2 == null && checkUser3 == null)
            {
                var newUser = new UserTable
                {
                    UsIdNumber = userDto.userIdNumber,
                    UsEmail = userDto.userEmail,
                    UsName = userDto.name,
                    UsPhone = userDto.phoneNumber,
                    UsRole = "U",
                    UsSurname = userDto.surname,
                    UsPasswordHash = HashAlgo.hashPassword(userDto.password)
                };
                //add to
                await _dbContext.UserTables.AddAsync(newUser);

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
            else
            {
                return OperationalResults.InvalidOperation;//we do not want to alert an attacker of a possible admin email
            }
        }
        //PUT

        public async Task<OperationalResults> UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            var user = await _dbContext.UserTables.FindAsync(id);
            if(user == null) return OperationalResults.NotFound;
            if(user.UsRole != "U") return OperationalResults.InvalidOperation;//This is a user-service only

            user.UsName = userDto.userName;
            user.UsSurname = userDto.userSurname;
            user.UsPasswordHash = HashAlgo.hashPassword(userDto.newPassword);
            user.UsEmail = userDto.userEmail;
            user.UsPhone = userDto.phoneNumber;
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
        public async Task<OperationalResults> DeleteUserByID(int id)
        {
            var user = await _dbContext.UserTables.FindAsync(id);
            if(user == null) return OperationalResults.NotFound;
            if(user.UsRole != "U") return OperationalResults.InvalidOperation;
            _dbContext.UserTables.Remove(user);
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

        public async Task<OperationalResults> DeleteUserByEmail(string email)
        {
            var user = await _dbContext.UserTables.FirstOrDefaultAsync(t => t.UsEmail == email);
            if(user == null) return OperationalResults.NotFound;
            if(user.UsRole != "U") return OperationalResults.InvalidOperation;

            _dbContext.UserTables.Remove(user);
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
        //PATCH

        public async Task<OperationalResults> PatchUser(PatchUserDto userDto, int userId)
        {
            try
            {
                var user = await _dbContext.UserTables.FindAsync(userId);
                if (user is null) return OperationalResults.NotFound;
                //update the user
                user.UsEmail = userDto.newUserEmail?? user.UsEmail;
                user.UsName = userDto.newUserName ?? user.UsName;
                user.UsPhone = userDto.newPhoneNumber ?? user.UsPhone;
                user.UsSurname = userDto.newUserSurname ?? user.UsSurname;
                if(userDto.newPassword != null)
                {
                    user.UsPasswordHash = HashAlgo.hashPassword(userDto.newPassword);
                }

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
