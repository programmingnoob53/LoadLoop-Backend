

using LoadLoop.api.Dtos;
using LoadLoop.api.Helper;
using LoadLoop.api.Services;

namespace LoadLoop.api.Extensions
{
    public static class UserExtension
    {
        private static string GetUserEndpoint = "GetUser";
        public static void MapUsersEndpoint(this WebApplication application)
        {
            var group = application.MapGroup("/users");
            //GET
            group.MapGet("/", async (UserService service) =>
            {
                return Results.Ok( await service.GetUsersAsync());
            });

            group.MapGet("/{id:int}", async (UserService service, int id) => { 
                var user = await service.GetUserByIDAsync(id);
                return user is null ? Results.NotFound(user) : Results.Ok(user);
            }).WithName(GetUserEndpoint);

            group.MapGet("/{email}", async (UserService service, string email) => {
                var user = await service.GetUserByEmailAsync(email);
                return user is null ? Results.NotFound() : Results.Ok(user);
            });
            //POST
            group.MapPost("/", async (UserService service, CreateUserDto userDto) => {
                var createUser = await service.registerUser(userDto);
                
                if(createUser == OperationalResults.AlreadyExits)
                {
                    return Results.Conflict("Email already exists");
                }else if(createUser == OperationalResults.Success)
                {
                    return Results.Created();
                }
                return Results.InternalServerError();
          
            });
            //DELETE
            group.MapDelete("/{id:int}", async (UserService service, int id) =>
            {
                var deleted = await service.deleteUserByIDAsync(id);
                return deleted switch
                {
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.NotFound => Results.NotFound(),
                    _=> Results.InternalServerError()//For the operation.failiure
                };

            });

            group.MapDelete("/", async (UserService service, string email) => { 
                var deleted = await service.deleteUserByEmailAsync(email);
                return deleted switch
                {
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.NotFound => Results.NotFound(),
                    _ => Results.InternalServerError()//For the operation.failiure
                };
            });

            //PUT
            group.MapPut("/", async (UserService service, UpdateUserDto userDto) => {
                var updateUser = await service.UpdateUserAsync(userDto);

                return updateUser switch
                {
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.NotFound => Results.NotFound(),
                    OperationalResults.AlreadyExits => Results.Conflict("Email already in use."),
                    _ => Results.InternalServerError()
                };
            });


        }
    }
}
