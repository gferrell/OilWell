using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ToolWell.Interfaces;

namespace ToolWell
{
    public class OilWellToolServiceClient : IOilWellToolServiceClient
    {
        private readonly HttpClient _httpClient;
        private IMessageService? _messageService;

        public OilWellToolServiceClient(string baseAddress, IMessageService messageService)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
            _messageService = messageService;
        }

        public async Task<IEnumerable<OilWellTool>> GetAllToolsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/OilWellTool");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<OilWellTool>>();
            }
            catch (Exception ex)
            {
                _messageService?.ShowMessage($"Service Error:{ex.Message}");
            }
            return null;

        }

        public async Task<OilWellTool> GetToolByIdAsync(string assetId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/OilWellTool/{{id}}?assetid={assetId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<OilWellTool>();
            }
            catch (Exception ex)
            {
                _messageService?.ShowMessage($"Service Error:{ex.Message}");
            }
            return null;
        }

        public async Task CreateToolAsync(OilWellTool tool)
        {

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/OilWellTool", tool);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _messageService?.ShowMessage($"Service Error:{ex.Message}");
            }
        }

        public async Task UpdateToolAsync(string assetId, OilWellTool tool)
        {

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/OilWellTool/{{id}}?assetid={assetId}", tool);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _messageService?.ShowMessage($"Service Error:{ex.Message}");
            }
        }

        public async Task DeleteToolAsync(string assetId)
        {

            try
            {
                var response = await _httpClient.DeleteAsync($"/api/OilWellTool/{{id}}?assetid={assetId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _messageService?.ShowMessage($"Service Error:{ex.Message}");
            }
        }
    }
}
