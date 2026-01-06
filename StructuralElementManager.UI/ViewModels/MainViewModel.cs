using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StructuralElementManager.BusinessLayer.Concrete;
using StructuralElementManager.DataAccessLayer.EntityFramework;
using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace StructuralElementManager.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        //Services
        private readonly StructuralColumnManager _columnService;
        private readonly StructuralMaterialManager _materialService;

        //Constructor
        public MainViewModel()
        {
            // Service'leri initialize et
            _columnService = new StructuralColumnManager(new EfColumnDal());
            _materialService = new StructuralMaterialManager(new EfMaterialDal());

            // Verileri yükle
            LoadData();
        }

        // ===== PROPERTIES =====

        [ObservableProperty]
        private ObservableCollection<StructuralColumn> _columns;
        [ObservableProperty]
        private ObservableCollection<StructuralMaterial> _materials;
        [ObservableProperty]
        private StructuralColumn? _selectedColumn;
        [ObservableProperty]
        private string _statusMessage = "Ready";

        // ===== COMMANDS =====

        [RelayCommand]
        private void LoadData()
        {
            var columnList = _columnService.TGetList();
            Columns = new ObservableCollection<StructuralColumn>(columnList);
            var materialList = _materialService.TGetList();
            Materials = new ObservableCollection<StructuralMaterial>(materialList);
            StatusMessage = $"Loaded {Columns.Count} columns, {Materials.Count} materials";
        }
        [RelayCommand]
        private void AddTestColumn()
        {
            //Test verisi ekle
            var newColumn = new StructuralColumn
            {
                Name = $"C{Columns.Count + 1}",
                Width = 50,
                Depth = 50,
                Height = 300,
                FloorLevel = 1,
                MaterialID = 1
            };
            _columnService.TAdd(newColumn);
            Columns.Add(newColumn);
            StatusMessage = $"Added column: {newColumn.Name}";
        }
        [RelayCommand]
        private void DeleteColumn()
        {
            if (SelectedColumn == null)
            {
                StatusMessage = "Please select a column to delete";
                return;
            }

            var deletedName = SelectedColumn.Name;

            _columnService.TDelete(SelectedColumn);
            Columns.Remove(SelectedColumn);

            StatusMessage = $"Deleted column: {deletedName}";
            SelectedColumn = null;
        }

        [RelayCommand]
        private void RefreshData()
        {
            LoadData();
        }
    }
}
