using LoadLoop.api.Dtos;
using LoadLoop.api.Helper;
using LoadLoop.api.Services;

namespace LoadLoop.api.Extensions
{
    public static class DriverExtensions
    {
        public static string GetDriversEndpointName = "DriverEndpointName";

        public static void MapDriverEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/drivers");
            //GET
            group.MapGet("/", async(DriverService driverService) =>
            {
                return Results.Ok(await driverService.GetDriversAsync());
            });
            group.MapGet("/{id:int}", async(DriverService driverService, int id) =>
            {
                var driver = await driverService.GetDriverByIDAsync(id);
                return driver == null ? Results.NotFound() : Results.Ok(driver);
            }).WithName(GetDriversEndpointName);

            group.MapGet("/{email}", async(DriverService driverService, string email) =>
            {
                var driver = await driverService.GetDriverByEmailAsync(email);
                return driver == null? Results.NotFound() : Results.Ok(driver);
            });
            //  POST

            group.MapPost("/", async(DriverService driverService, CreateDriverDto driverDto) =>
            {
                OperationalResults postResult = await driverService.CreateDriverAsync(driverDto);
                return postResult switch
                {
                    OperationalResults.Success => Results.Created(),
                    OperationalResults.AlreadyExists => Results.Problem("The user already exisits."),
                    _ => Results.InternalServerError()
                };
            });

            // PUT
            group.MapPut("/{id:int}", async(DriverService driverService, int id, UpadateDriverDto driverDto) =>
            {
                OperationalResults putResult = await driverService.UpdateDriverAsync(id, driverDto);
                return putResult switch
                {
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.NotFound => Results.NotFound(),
                    _ => Results.InternalServerError()
                };
            });

            // DELETE

            group.MapDelete("/{driverID:int}", async(UserService driverService, int driverID) =>
            {
                OperationalResults deleteResult = await driverService.DeleteUserByID(driverID);
                return deleteResult switch
                {
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.NotFound => Results.NotFound(),
                    _ => Results.InternalServerError()
                };
            });

            group.MapDelete("/user/{email}", async(DriverService driverService, string email) =>
            {
                OperationalResults deleteResult = await driverService.DeleteDriverByEmailAsync(email);
                return deleteResult switch
                {
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.NotFound => Results.NotFound(),
                    _ => Results.InternalServerError()
                };
            });
        }
    }
}