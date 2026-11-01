using LoadLoop.api.Services;
using LoadLoop.api.Helper;
using LoadLoop.api.Dtos;

namespace LoadLoop.api.Extensions
{
    public static class TruckExtension
    {
        private static string GetTruckNameEndpoint = "GetTruck";

        public static void MapTruckEndpoints(this WebApplication application)
        {
            var group = application.MapGroup("/Trucks");
            //GET endpoints
            group.MapGet("/", async (TruckService truckService) => {
                return Results.Ok(await truckService.GetTrucksAsync());
            });

            group.MapGet("/{truckID:int}", async (TruckService truckService, int truckID) =>
            {
                var truck = await truckService.GetTruckByIDAsync(truckID);
                return truck == null ? Results.NotFound() : Results.Ok(truck);
            }).WithName(GetTruckNameEndpoint);

            group.MapGet("/Users/{userID:int}", async (TruckService truckService, int userID) =>
            {
                var trucks = await truckService.GetTrucksByUserIDAsync(userID);
                return trucks.Any() ? Results.Ok(trucks) : Results.NotFound();
            });
            //DELETE endpoints
            group.MapDelete("/{truckID:int}", async (TruckService truckService, int truckID) =>
            {
                var deleteTruck = await truckService.deleteTruckByIDAsysnc(truckID);
                return deleteTruck switch
                {
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.NotFound => Results.NotFound(),
                    _ => Results.InternalServerError()
                };
            });

            group.MapDelete("/Users/{userID:int}", async(TruckService truckService, int userID) => {
                var deleteTruck = await truckService.deleteTrucksByUserAsync(userID);

                return deleteTruck switch
                {
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.NotFound => Results.NotFound(),
                    _ => Results.InternalServerError()
                };
            });
            //PUT endpoints
            group.MapPut("/", async (TruckService truckService, UpdateTruckDto truckDto) =>
            {
                var updateUser = await truckService.UpdateTruckAsync(truckDto);
                return updateUser switch
                {
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.NotFound => Results.NotFound(),
                    _ => Results.InternalServerError()
                };
            });
            group.MapPost("/", async (TruckService truckService, CreateTruckDto truckDto) =>
            {
                var createdTruck = await truckService.CreateTruckAsync(truckDto);
                return createdTruck switch
                {
                    OperationalResults.NotFound => Results.NotFound(),
                    OperationalResults.InvalidRole => Results.Problem("Invalid role"),
                    OperationalResults.Failiure => Results.InternalServerError(),
                    OperationalResults.Success => Results.Created(),
                    _ => Results.InternalServerError()//other case
                };
            });
        }
    }
}
