using Moq;
using ToolWell.ViewModel;
using ToolWell.Interfaces;
namespace ToolWell.Test
{
    public class OilWellToolViewModelTests
    {
        private readonly Mock<IOilWellToolServiceClient> _mockService;
        private readonly Mock<IMessageService> _mockMessageService;
        private readonly OilWellToolViewModel _viewModel;

        public OilWellToolViewModelTests()
        {
            // Mock the service
            _mockService = new Mock<IOilWellToolServiceClient>();
            _mockMessageService = new Mock<IMessageService>();
            // Create a list of fake tools
            var tools = new List<OilWellTool>
        {
            new OilWellTool { AssetId = "1", Type = ToolType.OpenHole },
            new OilWellTool { AssetId = "2", Type = ToolType.CasedHole}
        };

            // Setup the service to return the fake tools when GetAllTools is called
            _mockService.Setup(service => service.GetAllToolsAsync()).ReturnsAsync(tools);

            // Initialize ViewModel with the mocked service
            _viewModel = new OilWellToolViewModel(_mockService.Object, _mockMessageService.Object);
        }

        [Fact]
        public async Task LoadDataAsync_Populates_Tools()
        {
            // Act
            await _viewModel.LoadDataAsync();

            // Assert
            Assert.Equal(2, _viewModel.Tools.Count);
            Assert.Equal("1", _viewModel.Tools[0].AssetId);
            Assert.Equal("2", _viewModel.Tools[1].AssetId);
        }
    }
}