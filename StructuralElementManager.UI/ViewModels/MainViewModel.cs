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
        private void AddBeam()
        {
            try
            {
                var beamService = new StructuralBeamManager(new EfBeamDal());

                var newBeam = new StructuralBeam
                {
                    Name = $"B{GetNextBeamNumber()}",
                    Width = 30,
                    Height = 50,
                    Length = 600,
                    FloorLevel = CurrentFloor,
                    MaterialID = Materials.FirstOrDefault()?.MaterialId ?? 1
                };

                beamService.TAdd(newBeam);

                var floor = FloorLevels.FirstOrDefault(f => f.FloorNumber == CurrentFloor);
                if (floor == null)
                {
                    floor = new FloorLevelModel { FloorNumber = CurrentFloor };
                    FloorLevels.Add(floor);
                }

                floor.Elements.Add(new ElementTreeItemModel(newBeam));

                UpdateStatistics();
                StatusText = $"Added beam: {newBeam.Name} on Floor {CurrentFloor}";
            }
            catch (Exception ex)
            {
                StatusText = $"Error adding beam: {ex.Message}";
            }
        }

        [RelayCommand]
        private void AddSlab()
        {
            try
            {
                var slabService = new StructuralSlabManager(new EfSlabDal());

                var newSlab = new StructuralSlab
                {
                    Name = $"S{GetNextSlabNumber()}",
                    Length = 500,
                    Width = 400,
                    Thickness = 20,
                    FloorLevel = CurrentFloor,
                    MaterialID = Materials.FirstOrDefault()?.MaterialId ?? 1
                };

                slabService.TAdd(newSlab);

                var floor = FloorLevels.FirstOrDefault(f => f.FloorNumber == CurrentFloor);
                if (floor == null)
                {
                    floor = new FloorLevelModel { FloorNumber = CurrentFloor };
                    FloorLevels.Add(floor);
                }

                floor.Elements.Add(new ElementTreeItemModel(newSlab));

                UpdateStatistics();
                StatusText = $"Added slab: {newSlab.Name} on Floor {CurrentFloor}";
            }
            catch (Exception ex)
            {
                StatusText = $"Error adding slab: {ex.Message}";
            }
        }

        // Helper metodlar
        private int GetNextBeamNumber()
        {
            var allBeams = FloorLevels.SelectMany(f => f.Elements)
                .Where(e => e.Element is StructuralBeam)
                .Select(e => e.Element.Name)
                .ToList();

            int maxNumber = 0;
            foreach (var name in allBeams)
            {
                if (name.StartsWith("B") && int.TryParse(name.Substring(1), out int num))
                {
                    if (num > maxNumber) maxNumber = num;
                }
            }

            return maxNumber + 1;
        }

        private int GetNextSlabNumber()
        {
            var allSlabs = FloorLevels.SelectMany(f => f.Elements)
                .Where(e => e.Element is StructuralSlab)
                .Select(e => e.Element.Name)
                .ToList();

            int maxNumber = 0;
            foreach (var name in allSlabs)
            {
                if (name.StartsWith("S") && int.TryParse(name.Substring(1), out int num))
                {
                    if (num > maxNumber) maxNumber = num;
                }
            }

            return maxNumber + 1;
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

                // Database'den sil (tip kontrolü)
                if (element is StructuralColumn column)
                {
                    _columnService.TDelete(column);
                }
                else if (element is StructuralBeam beam)
                {
                    var beamService = new StructuralBeamManager(new EfBeamDal());
                    beamService.TDelete(beam);
                }
                else if (element is StructuralSlab slab)
                {
                    var slabService = new StructuralSlabManager(new EfSlabDal());
                    slabService.TDelete(slab);
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
                else if (SelectedElement is StructuralBeam beam)
                {
                    var beamService = new StructuralBeamManager(new EfBeamDal());
                    beamService.TUpdate(beam);
                }
                else if (SelectedElement is StructuralSlab slab)
                {
                    var slabService = new StructuralSlabManager(new EfSlabDal());
                    slabService.TUpdate(slab);
                }

                // Calculated values'ı yenile
                OnPropertyChanged(nameof(SelectedElementVolume));
                OnPropertyChanged(nameof(SelectedElementWeight));

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
        public string SelectedElementVolume
        {
            get
            {
                if (SelectedTreeItem?.Element == null) return "--";
                return SelectedTreeItem.Element.CalculateVolume().ToString("F3");
            }
        }

        public string SelectedElementWeight
        {
            get
            {
                if (SelectedTreeItem?.Element == null) return "--";
                return SelectedTreeItem.Element.CalculateWeight().ToString("F2");
            }
        }

        // Property changed handler için
        partial void OnSelectedTreeItemChanged(ElementTreeItemModel? value)
        {
            OnPropertyChanged(nameof(SelectedElement));
            OnPropertyChanged(nameof(SelectedElementVolume));  
            OnPropertyChanged(nameof(SelectedElementWeight));
        }
        public void DrawFloorPlan()
        {
            // Canvas'ı MainWindow'dan alman gerekir
            // Şimdilik placeholder
        }
    }
}