using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;
using System.Threading.Tasks;
using System;
using ToolWell.Interfaces;

namespace ToolWell.ViewModel
{
    public class OilWellToolViewModel : INotifyPropertyChanged
    {
        private readonly IOilWellToolServiceClient _serviceClient;
        private ObservableCollection<OilWellTool> _tools;
        private OilWellTool _selectedTool;
        private bool _isNewRecord;
        public OilWellToolViewModel(IOilWellToolServiceClient oilWellToolServiceClient, IMessageService messageService)
        {
            _serviceClient = oilWellToolServiceClient;  
            _isNewRecord = false;
            _messageService = messageService;
            LoadDataCommand = new RelayCommand(async () => await LoadDataAsync());
            AddToolCommand = new RelayCommand(AddTool);
            UpdateToolCommand = new RelayCommand(UpdateTool);
            DeleteToolCommand = new RelayCommand(DeleteTool);
            CancelCommand = new RelayCommand(Cancel);
#pragma warning disable 4014
            LoadDataAsync();  // We intentionally run this without awaiting.

#pragma warning restore 4014
        }

        public ObservableCollection<OilWellTool> Tools
        {
            get => _tools;
            set
            {
                _tools = value;
                OnPropertyChanged();
            }
        }
        public bool IsToolSelected => SelectedTool != null;

        public OilWellTool SelectedTool
        {
            get => _selectedTool;
            set
            {
                IsNewRecord = false;
                _selectedTool = value;
                OnPropertyChanged(nameof(IsToolSelected));
                OnPropertyChanged();
            }
        }

        public ICommand LoadDataCommand { get; }
        public ICommand AddToolCommand { get; }
        public ICommand UpdateToolCommand { get; }
        public ICommand DeleteToolCommand { get; }
        public ICommand CancelCommand { get; }
        private readonly IMessageService _messageService;

        public bool ContainsAssetId(string assetId)
        {
            return Tools.Any(tool => tool.AssetId == assetId);
        }

        public bool IsNewRecord { get => _isNewRecord;
            set
            {
                _isNewRecord = value;
                OnPropertyChanged();
            } 
        }
        private void Cancel()
        {
            SelectedTool = null;
        }
        public async Task LoadDataAsync()
        {
            var tools = await _serviceClient.GetAllToolsAsync();
            Tools = new ObservableCollection<OilWellTool>(tools);
        }

        private void AddTool()
        {
           
            SelectedTool= new OilWellTool {ServiceDateDue=DateTime.Now,AssetId="Tool Name" };
            IsNewRecord = true;
        }

        private async void UpdateTool()
        {
            if (SelectedTool == null)
            {
                _messageService.ShowMessage("Please select a tool to update.");
                return;
            }
            if (IsNewRecord)
            {
                //Do a little error checking on new record before saving it
                if (string.IsNullOrEmpty(SelectedTool.AssetId))
                {
                    _messageService.ShowMessage("AssetId must not be empty.");
                    return;
                }
                else if (ContainsAssetId(SelectedTool.AssetId))
                {
                    _messageService.ShowMessage("AssetId must be unique.");
                    return;
                }
                else if (SelectedTool.Type!=ToolType.CasedHole && SelectedTool.Type != ToolType.OpenHole)
                {
                    _messageService.ShowMessage("Please select a Tool Type.");
                    return;
                }
                if (string.IsNullOrEmpty(SelectedTool.Location))
                {
                    _messageService.ShowMessage("Location must not be empty.");
                    return;
                }
                await _serviceClient.CreateToolAsync(SelectedTool);
                IsNewRecord= false;
                Tools.Add(SelectedTool);
            }
            else
            {
                await _serviceClient.UpdateToolAsync(SelectedTool.AssetId, SelectedTool);
            }
            // Handle any post-update logic here, e.g., refreshing the list or showing a confirmation
        }

        private async void DeleteTool()
        {
            if (SelectedTool == null)
            {
                _messageService.ShowMessage("Please select a tool to delete.");
                return;
            }
            await _serviceClient.DeleteToolAsync(SelectedTool.AssetId);
            Tools.Remove(SelectedTool);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}