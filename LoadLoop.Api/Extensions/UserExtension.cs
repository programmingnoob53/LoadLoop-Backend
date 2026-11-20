using LoadLoop.api.Dtos;
using LoadLoop.api.Helper;
using LoadLoop.api.Services;

namespace LoadLoop.api.Extensions
{
    public static class UserExtension
    {
        private static string GetUserEndpointName = "GetUserEndpoint";
        public static void MapUserEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/users");
            group.MapGet("/", async(UserService userService) =>
            {
                return Results.Ok(await userService.GetUsersAsync());
            });
            group.MapGet("/{email}", async(UserService userService, string email) =>
            {
                var user = await userService.GetUserByEmail(email);
                return user switch
                {
                    null => Results.NotFound(),
                    _ => Results.Ok(user)
                };
            });
            group.MapGet("/{id:int}", async(UserService userService, int id) =>
            {
                var user = await userService.GetUserByIDAsync(id);
                return user switch
                {
                    null => Results.NotFound(),
                    _ => Results.Ok(user)
                };
            });
            //POST
            group.MapPost("/", async(UserService userService, CreateUserDto userDto) =>
            {
                OperationalResults postResult = await userService.CreateUserAsync(userDto);
                return postResult switch
                {
                    OperationalResults.Success => Results.Created(),
                    OperationalResults.InvalidOperation => Results.Problem("Cannot perform such an operation"),
                    _ => Results.InternalServerError()
                };
            }).WithName(GetUserEndpointName);
            //DELETE
            group.MapDelete("/{id:int}", async(UserService userService, int id) =>
            {
                OperationalResults deleteResult = await userService.DeleteUserByID(id);
                return deleteResult switch
                {
                    OperationalResults.NotFound => Results.NotFound(),
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.InvalidOperation => Results.Problem("Cannot perform such an action"),
                    _ => Results.InternalServerError()
                };
            });

            group.MapDelete("/{email}", async(UserService userService, string email) =>
            {
                OperationalResults deleteResult = await userService.DeleteUserByEmail(email);
                return deleteResult switch
                {
                    OperationalResults.NotFound => Results.NotFound(),
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.InvalidOperation => Results.Problem("Cannot perform such an action"),
                    _ => Results.InternalServerError()
                };
            });

            group.MapPut("/{id:int}", async(UserService userService, int id, UpdateUserDto userDto) =>
            {
                OperationalResults putResult = await userService.UpdateUserAsync(id, userDto);
                return putResult switch
                {
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.Failure => Results.InternalServerError(),
                    OperationalResults.InvalidOperation => Results.Problem("Invalid operation cannot be performed."),
                    _ => Results.NotFound()
                };
            });
        }
    }
}