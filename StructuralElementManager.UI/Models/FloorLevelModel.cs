using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.UI.Models
{
    public class FloorLevelModel
    {
        public int FloorNumber { get; set; }
        public string DisplayName => $"📁 Floor {FloorNumber}";
        public ObservableCollection<ElementTreeItemModel> Elements { get; set; }

        public FloorLevelModel()
        {
            Elements = new ObservableCollection<ElementTreeItemModel>();
        }
    }
}
