using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToolWell.Interfaces
{
    public interface IOilWellToolServiceClient
    {
        Task CreateToolAsync(OilWellTool tool);
        Task DeleteToolAsync(string assetId);
        Task<IEnumerable<OilWellTool>> GetAllToolsAsync();
        Task<OilWellTool> GetToolByIdAsync(string assetId);
        Task UpdateToolAsync(string assetId, OilWellTool tool);
    }
}