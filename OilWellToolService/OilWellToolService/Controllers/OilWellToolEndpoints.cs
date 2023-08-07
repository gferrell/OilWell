using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using OilWellToolService;
using OilWellToolService.Data;
namespace OilWellToolService.Controllers
{
    public static class OilWellToolEndpoints
    {
        public static void MapOilWellToolEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/OilWellTool").WithTags(nameof(OilWellTool));

            group.MapGet("/", async (OilWellToolServiceContext db) =>
            {
                return await db.OilWellTool.ToListAsync();
            })
            .WithName("GetAllOilWellTools")
            .WithOpenApi();

            group.MapGet("/{id}", async Task<Results<Ok<OilWellTool>, NotFound>> (string assetid, OilWellToolServiceContext db) =>
            {
                return await db.OilWellTool.AsNoTracking()
                    .FirstOrDefaultAsync(model => model.AssetId == assetid)
                    is OilWellTool model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
            .WithName("GetOilWellToolById")
            .WithOpenApi();

            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (string assetid, OilWellTool oilWellTool, OilWellToolServiceContext db) =>
            {
                var affected = await db.OilWellTool
                    .Where(model => model.AssetId == assetid)
                    .ExecuteUpdateAsync(setters => setters
                      .SetProperty(m => m.AssetId, oilWellTool.AssetId)
                      .SetProperty(m => m.Type, oilWellTool.Type)
                      .SetProperty(m => m.Weight, oilWellTool.Weight)
                      .SetProperty(m => m.Length, oilWellTool.Length)
                      .SetProperty(m => m.Diameter, oilWellTool.Diameter)
                      .SetProperty(m => m.ServiceDateDue, oilWellTool.ServiceDateDue)
                      .SetProperty(m => m.Location, oilWellTool.Location)
                    );

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("UpdateOilWellTool")
            .WithOpenApi();

            group.MapPost("/", async (OilWellTool oilWellTool, OilWellToolServiceContext db) =>
            {
                db.OilWellTool.Add(oilWellTool);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/OilWellTool/{oilWellTool.AssetId}", oilWellTool);
            })
            .WithName("CreateOilWellTool")
            .WithOpenApi();

            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (string assetid, OilWellToolServiceContext db) =>
            {
                var affected = await db.OilWellTool
                    .Where(model => model.AssetId == assetid)
                    .ExecuteDeleteAsync();

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("DeleteOilWellTool")
            .WithOpenApi();
        }
    }
}