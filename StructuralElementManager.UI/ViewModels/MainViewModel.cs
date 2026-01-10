using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using StructuralElementManager.EntityLayer.Concrete;
using StructuralElementManager.BusinessLayer.Concrete;
using StructuralElementManager.DataAccessLayer.EntityFramework;
using StructuralElementManager.UI.Models;
using System.Linq;

namespace StructuralElementManager.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // Services
        private readonly StructuralColumnManager _columnService;
        private readonly StructuralMaterialManager _materialService;

        public MainViewModel()
        {
            _columnService = new StructuralColumnManager(new EfColumnDal());
            _materialService = new StructuralMaterialManager(new EfMaterialDal());

            // Initialize
            FloorLevels = new ObservableCollection<FloorLevelModel>();
            Materials = new ObservableCollection<StructuralMaterial>();

            LoadData();
        }

        // ===== PROPERTIES =====

        [ObservableProperty]
        private ObservableCollection<FloorLevelModel> _floorLevels;

        [ObservableProperty]
        private ObservableCollection<StructuralMaterial> _materials;

        [ObservableProperty]
        private ElementTreeItemModel? _selectedTreeItem;

        [ObservableProperty]
        private int _currentFloor = 1;

        [ObservableProperty]
        private string _statusText = "Ready";

        // Seçili element (Properties panel için)
        public StructuralElement? SelectedElement => SelectedTreeItem?.Element;

        // İstatistikler
        [ObservableProperty]
        private int _totalElementCount;

        [ObservableProperty]
        private double _totalVolume;

        // ===== COMMANDS =====

        [RelayCommand]
        private void LoadData()
        {
            try
            {
                // Malzemeleri yükle
                var materialList = _materialService.TGetList();
                Materials = new ObservableCollection<StructuralMaterial>(materialList);

                // Kolonları yükle ve floor'lara göre grupla
                var allColumns = _columnService.TGetList();

                FloorLevels.Clear();

                // Floor'ları bul (unique)
                var floors = allColumns.Select(c => c.FloorLevel).Distinct().OrderBy(f => f);

                foreach (var floor in floors)
                {
                    var floorModel = new FloorLevelModel { FloorNumber = floor };

                    var columnsOnFloor = allColumns.Where(c => c.FloorLevel == floor);
                    foreach (var column in columnsOnFloor)
                    {
                        floorModel.Elements.Add(new ElementTreeItemModel(column));
                    }

                    FloorLevels.Add(floorModel);
                }

                // İstatistikleri güncelle
                UpdateStatistics();

                StatusText = $"Loaded {allColumns.Count} columns, {Materials.Count} materials";
            }
            catch (Exception ex)
            {
                StatusText = $"Error loading data: {ex.Message}";
            }
        }

        [RelayCommand]
        private void AddColumn()
        {
            try
            {
                // Yeni kolon oluştur
                var newColumn = new StructuralColumn
                {
                    Name = $"C{GetNextColumnNumber()}",
                    Width = 50,
                    Depth = 50,
                    Height = 300,
                    FloorLevel = CurrentFloor,
                    MaterialID = Materials.FirstOrDefault()?.MaterialId ?? 1
                };

                // Database'e ekle
                _columnService.TAdd(newColumn);

                // Tree view'e ekle
                var floor = FloorLevels.FirstOrDefault(f => f.FloorNumber == CurrentFloor);
                if (floor == null)
                {
                    floor = new FloorLevelModel { FloorNumber = CurrentFloor };
                    FloorLevels.Add(floor);
                }

                floor.Elements.Add(new ElementTreeItemModel(newColumn));

                UpdateStatistics();
                StatusText = $"Added column: {newColumn.Name} on Floor {CurrentFloor}";
            }
            catch (Exception ex)
            {
                StatusText = $"Error adding column: {ex.Message}";
            }
        }

        [RelayCommand]
        private void DeleteElement()
        {
            if (SelectedTreeItem == null)
            {
                StatusText = "Please select an element to delete";
                return;
            }

            try
            {
                var element = SelectedTreeItem.Element;
                var elementName = element.Name;

                // Database'den sil
                if (element is StructuralColumn column)
                {
                    _columnService.TDelete(column);
                }

                // Tree view'den kaldır
                var floor = FloorLevels.FirstOrDefault(f => f.FloorNumber == element.FloorLevel);
                if (floor != null)
                {
                    floor.Elements.Remove(SelectedTreeItem);
                }

                UpdateStatistics();
                StatusText = $"Deleted element: {elementName}";

                SelectedTreeItem = null;
            }
            catch (Exception ex)
            {
                StatusText = $"Error deleting element: {ex.Message}";
            }
        }

        [RelayCommand]
        private void UpdateElement()
        {
            if (SelectedElement == null)
            {
                StatusText = "Please select an element to update";
                return;
            }

            try
            {
                if (SelectedElement is StructuralColumn column)
                {
                    _columnService.TUpdate(column);
                }

                UpdateStatistics();
                StatusText = $"Updated element: {SelectedElement.Name}";
            }
            catch (Exception ex)
            {
                StatusText = $"Error updating element: {ex.Message}";
            }
        }

        [RelayCommand]
        private void RefreshData()
        {
            LoadData();
        }

        [RelayCommand]
        private void SelectFloor(int floorNumber)
        {
            CurrentFloor = floorNumber;
            StatusText = $"Selected Floor {floorNumber}";
        }

        // ===== HELPER METHODS =====

        private int GetNextColumnNumber()
        {
            var allColumns = FloorLevels.SelectMany(f => f.Elements)
                .Where(e => e.Element is StructuralColumn)
                .Select(e => e.Element.Name)
                .ToList();

            int maxNumber = 0;
            foreach (var name in allColumns)
            {
                if (name.StartsWith("C") && int.TryParse(name.Substring(1), out int num))
                {
                    if (num > maxNumber) maxNumber = num;
                }
            }

            return maxNumber + 1;
        }

        private void UpdateStatistics()
        {
            var allElements = FloorLevels.SelectMany(f => f.Elements).ToList();

            TotalElementCount = allElements.Count;
            TotalVolume = allElements.Sum(e => e.Element.CalculateVolume());
        }

        // Property changed handler için
        partial void OnSelectedTreeItemChanged(ElementTreeItemModel? value)
        {
            OnPropertyChanged(nameof(SelectedElement));
        }
    }
}