using LoadLoop.api.Services;
using LoadLoop.api.Helper;
using LoadLoop.api.Dtos;

namespace LoadLoop.api.Extensions
{
    public static class TruckExtension
    {
        private static string GetTruckEndpointName = "GetTruckEndpoint";
       public static void MapTruckEndPoints(this WebApplication app)
        {
            
            var group = app.MapGroup("/trucks");
            //GET
            group.MapGet("/", async(TruckService truckService) =>
            {
                return Results.Ok(await truckService.GetTrucksAsync());
            }).WithName(GetTruckEndpointName);

            group.MapGet("/user/{userId:int}", async(TruckService truckService, int userId) =>
            {
                var trucks = await truckService.GetTrucksByUserIDAsync(userId);
                return Results.Ok(trucks);
            });

            group.MapGet("/{id:int}", async(TruckService truckService, int id) =>
            {
                var truck = await truckService.GetTruckByIDAsync(id);
                return truck == null ? Results.NotFound() : Results.Ok(truck);
            });
            //POST
            group.MapPost("/", async(TruckService truckService, CreateTruckDto truckDto) =>
            {
                OperationalResults result = await truckService.CreateTruckAsync(truckDto);
                return result switch
                {
                    OperationalResults.NotFound => Results.NotFound(),
                    OperationalResults.InvalidRole => Results.Problem("Invalid role for the user"),//user was found but invalid role
                    OperationalResults.Success => Results.NoContent(),
                    _ => Results.InternalServerError() 
                };
            });
            //PUT

            group.MapPut("/{id:int}", async(TruckService truckService,int id,  UpdateTruckDto truckDto) =>
            {
                OperationalResults updateTruck = await truckService.UpdateTruckAsyc(id, truckDto);
                return updateTruck switch
                {
                    OperationalResults.NotFound => Results.NotFound(),
                    OperationalResults.Failure => Results.InternalServerError(),
                    _ => Results.NoContent() //the last success case
                };
            });

            //DELETE

            group.MapDelete("/{id:int}", async(TruckService truckService, int id) =>
            {
                OperationalResults deleteTruck = await truckService.DeleteTruckAsync(id);

                return deleteTruck switch
                {
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.NotFound => Results.NotFound(),
                    _ => Results.InternalServerError()
                };
            });

            group.MapDelete("/users/{id:int}", async(TruckService truckService, int userid) =>
            {
                OperationalResults deleteTruck = await truckService.DeleteTrucksByUserIDAsync(userid);

                return deleteTruck switch
                {
                    OperationalResults.Success => Results.NoContent(),
                    OperationalResults.NotFound => Results.NotFound(),
                    _ => Results.InternalServerError()
                };
            });
            //PATCH
            group.MapPatch("/{id:int}", async (TruckService truckService, int id, PatchTruckDto truckDto) => {
                var patchResult = await truckService.PatchTruck(id, truckDto);

                return patchResult switch
                { 
                    OperationalResults.NotFound => Results.NotFound(),
                    OperationalResults.Success => Results.NoContent(),
                    _ => Results.InternalServerError()
                };
            });
        }
    
    }
}
